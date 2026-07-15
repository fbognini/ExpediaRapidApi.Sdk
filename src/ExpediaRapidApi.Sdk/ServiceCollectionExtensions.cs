using ExpediaRapidApi.Sdk.Activities;
using ExpediaRapidApi.Sdk.Lodging;
using ExpediaRapidApi.Sdk.Pay;
using fbognini.Sdk.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace ExpediaRapidApi.Sdk
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExpediaRapidApiService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ExpediaRapidApiSettings>(configuration.GetSection(nameof(ExpediaRapidApiSettings)));

            // One token cache per OAuth2 client: Activities is authorized by a different client than Cars and Pay.
            services
                .AddSingleton<IExpediaCurrentUserService, ExpediaCurrentUserService>()
                .AddSingleton<IExpediaActivitiesCurrentUserService, ExpediaActivitiesCurrentUserService>();

            services.AddScoped<ExpediaLodgingAuthorizationHttpMessageHandler>();
            services.AddHttpClient<IExpediaLodgingApiClient, ExpediaLodgingApiClient>()
                .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                })
                .AddHttpMessageHandler<ExpediaLodgingAuthorizationHttpMessageHandler>()
                .ThrowApiExceptionIfNotSuccess()
                .AddLogging();

            services.AddHttpClient<IExpediaCarsApiClient, ExpediaCarsApiClient>()
                .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                })
                .ThrowApiExceptionIfNotSuccess()
                .AddAuthenticationPolicy<IExpediaCurrentUserService>()
                .AddLogging();

            services.AddHttpClient<IExpediaPayApiClient, ExpediaPayApiClient>()
                .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                })
                .ThrowApiExceptionIfNotSuccess()
                .AddAuthenticationPolicy<IExpediaCurrentUserService>()
                .AddLogging();

            services.AddHttpClient<IExpediaActivitiesApiClient, ExpediaActivitiesApiClient>()
                .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                })
                .ThrowApiExceptionIfNotSuccess()
                .AddAuthenticationPolicy<IExpediaActivitiesCurrentUserService>()
                .AddLogging();

            services.AddHttpClient<IExpediaAuthenticationApiService, ExpediaAuthenticationService>()
                .ThrowApiExceptionIfNotSuccess()
                .AddLogging();

            // Root SDK
            services.AddScoped<ExpediaRapidApiClient>();

            return services;
        }
    }
}
