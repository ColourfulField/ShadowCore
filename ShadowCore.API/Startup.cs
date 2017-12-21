using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using ShadowCore.API.Configuration.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using ShadowCore.DI;
using ShadowBox.AutomaticDI;

namespace ShadowCore.API
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
            var assemblyNames = Configuration.GetSection("AssemblyNamesForDIAutoRegistration").Get<string[]>();
            var runtimeLibraries = DependencyContext.Default.RuntimeLibraries.Where(a => assemblyNames.Any(x => a.Name.Contains(x)));
            builder.RegisterModule(new AutoRegistrationModule(runtimeLibraries));

            builder.RegisterModule(new ManualRegistrationModule());
        }
    }
}