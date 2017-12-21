using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Serilog;

namespace ShadowCore.API.Configuration.Extensions
{
    /// <summary>
    /// Contains shorthand methods for HTTP request pipeline configuration in Startup
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configures localization middleware. Contains the list of supported cultures
        /// </summary>
        /// <param name="app"></param>
        public static void UseLocalization(this IApplicationBuilder app)
        {
            var supportedCultures = new List<CultureInfo>
                                    {
                                        new CultureInfo("en-US"),
                                        new CultureInfo("en-AU"),
                                        new CultureInfo("en-GB"),
                                        new CultureInfo("es-ES"),
                                        new CultureInfo("ja-JP"),
                                        new CultureInfo("fr-FR"),
                                        new CultureInfo("zh"),
                                        new CultureInfo("zh-CN")
                                    };

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(localizationOptions);
        }

        /// <summary>
        /// Configures Serilog - middleware for structured logging.
        /// </summary>
        /// <param name="app"></param>
        public static void UseSerilog(this IApplicationBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext() // ensures that any events written directly through Serilog will seamlessly pick up correlation ids like RequestId from ASP.NET
              //.WriteTo.Logger()
                .CreateLogger();
        }

        /// <summary>
        /// Configures Swagger and Swashbuckle middleware
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwashbuckle(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x =>
                             {
                                 x.SwaggerEndpoint("/swagger/v1/swagger.json", "ShadowCore API V1");
                                 x.SwaggerEndpoint("/swagger/v0/swagger.json", "ShadowCore API V0");                                 

                                 x.DocumentTitle("ShadowCore");
                                 x.ShowJsonEditor();
                                 x.ShowRequestHeaders();
                                 x.ConfigureOAuth2("swagger-ui", "swagger-ui-secret", "swagger-ui-realm", "Swagger UI");
                             });
        }
    }
}
