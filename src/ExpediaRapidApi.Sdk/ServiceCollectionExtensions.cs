using ExpediaRapidApi.Sdk.Handlers;
using fbognini.Sdk.Extensions;
using fbognini.Sdk.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace ExpediaRapidApi.Sdk
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExpediaRapidApiService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ExpediaRapidApiSettings>(configuration.GetSection(nameof(ExpediaRapidApiSettings)));

            services.AddScoped<ExpediaAuthorizationHttpMessageHandler>();

            services.AddHttpClient<IExpediaRapidApiService, ExpediaRapidApiService>()
                .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                })
                .AddHttpMessageHandler<ExpediaAuthorizationHttpMessageHandler>()
                .ThrowApiExceptionIfNotSuccess()
                .AddLogging();

            return services;
        }
    }
}
