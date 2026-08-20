using ExpediaRapidApi.Sdk.Activities;
using ExpediaRapidApi.Sdk.Cars;
using ExpediaRapidApi.Sdk.Lodging;
using ExpediaRapidApi.Sdk.Pay;
using ExpediaRapidApi.Sdk.Shared;
using fbognini.Sdk;
using fbognini.Sdk.Interfaces;
using fbognini.Sdk.Models;
using fbognini.Sdk.Utils;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk;


public class ExpediaRapidApiClient(IExpediaLodgingApiClient lodging, IExpediaCarsApiClient cars, IExpediaPayApiClient pay, IExpediaActivitiesApiClient activities)
{
    public IExpediaLodgingApiClient Lodging { get; } = lodging;
    public IExpediaCarsApiClient Cars { get; } = cars;
    public IExpediaPayApiClient Pay { get; } = pay;
    public IExpediaActivitiesApiClient Activities { get; } = activities;
}


public class ExpediaBaseApiClient : BaseApiService
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        Converters = { new JsonStringEnumConverter() }
    };

    private const string PaginationTokenParameter = "pagination_token";

    public QueryStringBuilderFromJsonOptions? QueryStringBuilderFromJsonOptions { get; }
    public ExpediaRapidApiSettings Settings { get; }

    public ExpediaBaseApiClient(HttpClient client, IOptions<ExpediaRapidApiSettings> settings, ISdkCurrentUserService? currentUserService)
        : base(client, currentUserService, JsonSerializerOptions)
    {
        Settings = settings.Value;
        QueryStringBuilderFromJsonOptions = new QueryStringBuilderFromJsonOptions()
        {
            ArrayFormatterType = ArrayFormatterType.None,
            JsonSerializerOptions = JsonSerializerOptions
        };

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.AcceptEncoding.Clear();
        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        client.DefaultRequestHeaders.UserAgent.ParseAdd("GHC/2019");
    }

    /// <summary>
    /// Issues a GET and wraps the payload together with the pagination metadata Rapid returns in the headers.
    /// <see cref="ExpediaPaginationResponse{T}.NextPageLink"/> is ready to be requested as it is to walk to the next page.
    /// </summary>
    protected async Task<ExpediaPaginationResponse<T>> GetPaginatedApi<T>(string url, RequestOptions? requestOptions = null, CancellationToken cancellationToken = default)
    {
        var response = await GetApiAsync(url, requestOptions, cancellationToken);

        // The Lodging implementation this replaces read the body with the default Web options, so every DTO that
        // went through it needed explicit [JsonPropertyName] attributes. Deserialize with the SDK options instead.
        var json = await response.Content.ReadFromJsonAsync<T>(JsonSerializerOptions, cancellationToken: cancellationToken);

        return new ExpediaPaginationResponse<T>()
        {
            NextPageLink = GetNextPageLink(response, url),
            TotalResults = GetTotalResults(response),
            Response = json!
        };
    }

    /// <summary>
    /// Reads the IETF Link header and returns the url of the next page, or null on the last page.
    /// </summary>
    // Lodging hands back a complete, public url that can be followed as it is.
    // Activities answers with the internal host that served the page (*.exp-aws.net), unreachable from outside and without the angle brackets and the rel attribute the header format prescribes: of that url only the pagination_token is usable, so it is moved onto the url we requested.
    private static string? GetNextPageLink(HttpResponseMessage response, string requestUrl)
    {
        if (!response.Headers.TryGetValues("Link", out var links))
        {
            return null;
        }

        var nextPageLink = FindNextLink(links);
        if (nextPageLink is null)
        {
            return null;
        }

        var paginationToken = GetQueryParameter(nextPageLink, PaginationTokenParameter);

        return paginationToken is null ? nextPageLink : WithPaginationToken(requestUrl, paginationToken);
    }

    private static string? FindNextLink(IEnumerable<string> links)
    {
        string? onlyUrl = null;
        var urlCount = 0;

        foreach (var link in links.SelectMany(x => x.Split(',')))
        {
            var url = ExtractUrl(link);
            if (url is null)
            {
                continue;
            }

            if (link.Contains("rel=\"next\"", StringComparison.OrdinalIgnoreCase))
            {
                return url;
            }

            onlyUrl = url;
            urlCount++;
        }

        // Some endpoints send the next link without the rel attribute. A lone unlabelled link can only be the next one.
        return urlCount == 1 ? onlyUrl : null;

        static string? ExtractUrl(string link)
        {
            var from = link.IndexOf('<');
            if (from >= 0)
            {
                var to = link.IndexOf('>', from);
                return to > from + 1 ? link[(from + 1)..to] : null;
            }

            // Activities sends the url bare, with neither angle brackets nor attributes.
            var bare = link.Split(';')[0].Trim();
            return bare.StartsWith("http", StringComparison.OrdinalIgnoreCase) ? bare : null;
        }
    }

    /// <summary>
    /// Reads a query parameter off a url without going through Uri, which would re-encode a token Rapid expects to receive back exactly as it was sent.
    /// </summary>
    private static string? GetQueryParameter(string url, string name)
    {
        var queryStart = url.IndexOf('?');
        if (queryStart < 0)
        {
            return null;
        }

        foreach (var parameter in url[(queryStart + 1)..].Split('&'))
        {
            var separator = parameter.IndexOf('=');
            if (separator > 0 && parameter[..separator].Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return parameter[(separator + 1)..];
            }
        }

        return null;
    }

    private static string WithPaginationToken(string url, string paginationToken)
    {
        var queryStart = url.IndexOf('?');
        var path = queryStart < 0 ? url : url[..queryStart];

        IEnumerable<string> parameters = queryStart < 0
            ? []
            : url[(queryStart + 1)..]
                .Split('&')
                .Where(x => !string.IsNullOrEmpty(x) && GetQueryParameter($"?{x}", PaginationTokenParameter) is null);

        return $"{path}?{string.Join('&', parameters.Append($"{PaginationTokenParameter}={paginationToken}"))}";
    }

    /// <summary>
    /// Reads the Pagination-Total-Results header, which Activities does not send as the bare number its name promises but as total results=180: parsed as it is, the total is always null.
    /// </summary>
    private static int? GetTotalResults(HttpResponseMessage response)
    {
        if (!response.Headers.TryGetValues("Pagination-Total-Results", out var values))
        {
            return null;
        }

        var value = values.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (int.TryParse(value, out var total))
        {
            return total;
        }

        var digits = new string([.. value.Where(char.IsDigit)]);

        return int.TryParse(digits, out total) ? total : null;
    }

    protected RequestOptions GetRequestOptions(object? options)
    {
        var _options = new RequestOptions();
        if (options is not null && options is IHasCustomerHeaderOptions customerOptions && customerOptions.Customer is not null)
        {
            _options.Headers.Add("Customer-Ip", customerOptions.Customer.CustomerIp);

            if (!string.IsNullOrWhiteSpace(customerOptions.Customer.UserAgent))
            {
                _options.Headers.UserAgent.Clear();
                _options.Headers.UserAgent.ParseAdd(customerOptions.Customer.UserAgent);
            }

            if (!string.IsNullOrWhiteSpace(customerOptions.Customer.CustomerSessionId))
            {
                _options.Headers.Add("Customer-Session-Id", customerOptions.Customer.CustomerSessionId);
            }
        }

        if (options is IHasTestHeaderOptions testOptions)
        {
            // Fall back to the configured value, so that staging cannot forget the header and get an opaque 500
            // out of the Activities price check.
            var test = string.IsNullOrWhiteSpace(testOptions.Test) ? Settings.TestHeader : testOptions.Test;

            if (!string.IsNullOrWhiteSpace(test))
            {
                _options.Headers.Add("Test", test);
            }
        }

        return _options;
    }
}