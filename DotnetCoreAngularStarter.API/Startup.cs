using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Autofac;
using DotnetCoreAngularStarter.API.Initialization;
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
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.ConfigureLocalization();
            services.ConfigureAppOptions(Configuration);
            services.ConfigureAutomaticMapper(maxDepth: 5);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseLocalization();
            app.UseSerilog();

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