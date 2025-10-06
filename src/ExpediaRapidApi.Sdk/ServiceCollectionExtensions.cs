using ExpediaRapidApi.Sdk.Cars;
using ExpediaRapidApi.Sdk.Cars.GetCarDetails;
using ExpediaRapidApi.Sdk.Lodging;
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

            
            services
                .AddSingleton<IExpediaCurrentUserService, ExpediaCurrentUserService>();
            
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


            services.AddHttpClient<IExpediaAuthenticationApiService, ExpediaAuthenticationService>()
                .ThrowApiExceptionIfNotSuccess()
                .AddLogging();

            // Root SDK
            services.AddTransient<ExpediaRapidApiClient>();

            return services;
        }
    }
}
