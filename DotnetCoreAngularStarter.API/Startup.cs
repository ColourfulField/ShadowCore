using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using DotnetCoreAngularStarter.API.Configuration.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using DotnetCoreAngularStarter.DI;
using ShadowBox.AutomaticDI;

namespace DotnetCoreAngularStarter.API
{
    [SuppressMessage("", "CS1591:MissingXmlDocumentation")]
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
            services.AddBearerTokenAuthentication();
            services.AddSwagger();
            services.ConfigureLocalization();
            services.ConfigureAppOptions(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseSerilog();
            app.UseLocalization();
            app.UseAuthentication();
            app.UseSwashbuckle();

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