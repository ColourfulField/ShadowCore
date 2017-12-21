using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Serilog;

namespace DotnetCoreAngularStarter.API.Initialization
{
    public static class ApplicationBuilderExtensions
    {
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

        public static void UseSerilog(this IApplicationBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext() // ensures that any events written directly through Serilog will seamlessly pick up correlation ids like RequestId from ASP.NET
                //.WriteTo.Logger()
                .CreateLogger();
        }
    }
}
