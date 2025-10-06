using ExpediaRapidApi.Sdk.Cars;
using ExpediaRapidApi.Sdk.Lodging;
using fbognini.Sdk.Extensions;
using fbognini.Sdk.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;

namespace ExpediaRapidApi.Sdk
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExpediaRapidApiService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ExpediaRapidApiSettings>(configuration.GetSection(nameof(ExpediaRapidApiSettings)));

            //services.AddScoped<ExpediaLodgingAuthorizationHttpMessageHandler>();

            //services.AddHttpClient<IExpediaRapidApiService, ExpediaRapidLodgingApiService>()
            //    .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
            //    {
            //        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            //    })
            //    .AddHttpMessageHandler<ExpediaLodgingAuthorizationHttpMessageHandler>()
            //    .ThrowApiExceptionIfNotSuccess()
            //    .AddLogging();

            services.AddHttpClient<IExpediaLodgingApiClient, ExpediaLodgingApiClient>()
                .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                })
                .AddHttpMessageHandler<ExpediaLodgingAuthorizationHttpMessageHandler>()
                .ThrowApiExceptionIfNotSuccess()
                .AddLogging();

            services.AddHttpClient<IExpediaCarsApiClient, ExpediaCarsApiClient>()
                //.ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
                //{
                //    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                //})
                .AddHttpMessageHandler<ExpediaCarsAuthorizationHttpMessageHandler>()
                .ThrowApiExceptionIfNotSuccess()
                .AddLogging();

            // Root SDK
            services.AddTransient<ExpediaRapidApiClient>();

            return services;
        }
    }
}
