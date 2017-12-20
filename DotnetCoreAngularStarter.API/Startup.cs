using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Localization;
using DotnetCoreAngularStarter.Common.Options;
using DotnetCoreAngularStarter.ManualDI;
using Serilog;
using ShadowBox.AutomaticDI;
using ShadowBox.Mapper.Extensions;
using ShadowBox.Utilities.Localization;

namespace DotnetCoreAngularStarter.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext() // ensures that any events written directly through Serilog will seamlessly pick up correlation ids like RequestId from ASP.NET
                //.WriteTo.Logger()
                .CreateLogger();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            #region Localization

            services.Configure<ClassLibraryLocalizationOptions>(
                options =>
                {
                    options.ResourcePaths = new Dictionary<string, string>
                    {
                        {"DotnetCoreAngularStarter.BusinessLogic", "Localization"},
                        {"DotnetCoreAngularStarter.API", "Localization"}
                    };
                    options.UseSeparateFolderForEachResource = true;
                });
            services.TryAddSingleton(typeof(IStringLocalizerFactory), typeof(ClassLibraryStringLocalizerFactory));
            services.AddLocalization();
            //services.Configure<RequestLocalizationOptions>(
            //    opts =>
            //    {
            //        var supportedCultures = new List<CultureInfo>
            //        {
            //            new CultureInfo("en-GB"),
            //            new CultureInfo("fr"),
            //        };

            //        opts.DefaultRequestCulture = new RequestCulture("en-GB");
            //        // Formatting numbers, dates, etc.
            //        opts.SupportedCultures = supportedCultures;
            //        // UI strings that we have localized.
            //        opts.SupportedUICultures = supportedCultures;
            //    });

            #endregion
            
            services.Configure<DatabaseOptions>(Configuration.GetSection("DatabaseOptions"));
            services.ConfigureMapper(5);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            #region Localization

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

            #endregion

            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //TODO change solutionName key to be array
            var runtimeLibraries = DependencyContext.Default.RuntimeLibraries
                .Where(a => a.Name.Contains(Configuration["SolutionName"]) || a.Name.Contains("ShadowBox"));
            builder.RegisterModule(new AutoRegistrationModule(runtimeLibraries));
            builder.RegisterModule(new ManualRegistrationModule());
        }
    }
}