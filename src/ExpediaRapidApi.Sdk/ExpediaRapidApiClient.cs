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

    public QueryStringBuilderFromJsonOptions? QueryStringBuilderFromJsonOptions { get; }
    public ExpediaRapidApiSettings Settings { get; }

    public ExpediaBaseApiClient(HttpClient client, IOptions<ExpediaRapidApiSettings> settings, ISdkCurrentUserService? currentUserService)
        : base(client, currentUserService, JsonSerializerOptions)
    {
        Settings = settings.Value;
        QueryStringBuilderFromJsonOptions = new QueryStringBuilderFromJsonOptions()
        {
            UseIndexForArrays = false,
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
    /// Follow <see cref="ExpediaPaginationResponse{T}.NextPageLink"/> verbatim to walk to the next page.
    /// </summary>
    protected async Task<ExpediaPaginationResponse<T>> GetPaginatedApi<T>(string url, RequestOptions? requestOptions = null, CancellationToken cancellationToken = default)
    {
        var response = await GetApiAsync(url, requestOptions, cancellationToken);

        // The Lodging implementation this replaces read the body with the default Web options, so every DTO that
        // went through it needed explicit [JsonPropertyName] attributes. Deserialize with the SDK options instead.
        var json = await response.Content.ReadFromJsonAsync<T>(JsonSerializerOptions, cancellationToken: cancellationToken);

        return new ExpediaPaginationResponse<T>()
        {
            NextPageLink = GetNextPageLink(response),
            TotalResults = GetTotalResults(response),
            Response = json!
        };
    }

    /// <summary>
    /// Reads the IETF Link header and returns the URL flagged <c>rel="next"</c>, or null on the last page.
    /// </summary>
    private static string? GetNextPageLink(HttpResponseMessage response)
    {
        if (!response.Headers.TryGetValues("Link", out var links))
        {
            return null;
        }

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
            var to = link.IndexOf('>');

            return from >= 0 && to > from + 1 ? link[(from + 1)..to] : null;
        }
    }

    private static int? GetTotalResults(HttpResponseMessage response)
    {
        if (response.Headers.TryGetValues("Pagination-Total-Results", out var values) &&
            int.TryParse(values.FirstOrDefault(), out var total))
        {
            return total;
        }

        return null;
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