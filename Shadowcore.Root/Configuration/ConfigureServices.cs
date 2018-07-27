using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using ShadowTools.Utilities.Localization;
using Swashbuckle.AspNetCore.Swagger;
using ShadowCore.BusinessLogic.Services.Abstract;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shadowcore.Root.Configuration
{
    /// <summary>
    /// Contains shorthand methods for service configuration in Startup
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Configures localizer factory options and adds factory it as a singleton to service collection
        /// </summary>
        /// <param name="services">Service collection object to configure</param>
        public static void ConfigureLocalization(this IServiceCollection services)
        {
            services.Configure<ClassLibraryLocalizationOptions>(options =>
            {
                options.ResourcePaths = new Dictionary<string, string>
                                        {
                                            {
                                                "ShadowCore.BusinessLogic",
                                                "Localization"
                                            },
                                            {
                                                "Shadowcore.Root",
                                                "Localization"
                                            }
                                        };
                options.UseSeparateFolderForEachResource = true;
            });
            services.TryAddSingleton(typeof(IStringLocalizerFactory), typeof(ClassLibraryStringLocalizerFactory));
            services.AddLocalization();
        }

        /// <summary>
        /// Adds and configures swagger API documentation
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services)
        {
            var xmlCommentsFilePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Shadowcore.Root.xml");

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info {Title = "ShadowCore API v1", Version = "v1"});
                x.SwaggerDoc("v0", new Info {Title = "ShadowCore API v0", Version = "v0"});

                x.IncludeXmlComments(xmlCommentsFilePath);
                x.DocInclusionPredicate((documentName, apiDescription) =>
                {
                    var versions = apiDescription.AllControllerAttributes()
                                                 .OfType<ApiVersionAttribute>()
                                                 .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == documentName);
                });

                x.AddSecurityDefinition("oauth2", new OAuth2Scheme
                                                  {
                                                      Flow = "implicit",
                                                      AuthorizationUrl = "http://localhost:64444/api/v1/Authorization/token",
                                                      TokenUrl = "http://localhost:64444/api/v1/Authorization/token"
                                                  }
                );

                //x.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        /// <summary>
        /// Adds and configures Bearer token authentication
        /// </summary>
        /// <param name="services"></param>
        public static void AddBearerTokenAuthentication(this IServiceCollection services)
        {
            // We need to register and resolve this service here, because at this point
            // we don't have access to Autofac registrations
            // services.AddSingleton<IBearerTokenService, BearerTokenService>();
            // var serviceProvider = services.BuildServiceProvider().GetService<IBearerTokenService>();
            //  var tokenService = serviceProvider.GetService<IBearerTokenService>();

            //services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
            var serviceProvider = services.BuildServiceProvider();
            var tokenService = serviceProvider.GetService<IBearerTokenService>();
            services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
                    {
                        cfg.TokenValidationParameters = new TokenValidationParameters
                                                        {
                                                            ValidateIssuer = false,
                                                            ValidateAudience = false,
                                                            ValidateLifetime = false,
                                                            ValidateIssuerSigningKey = true,

                                                            ValidIssuer = "Fiver.Security.Bearer",
                                                            ValidAudience = "Fiver.Security.Bearer",
                                                            IssuerSigningKey = tokenService.GenerateSingingKey()
                                                        };

                    });
        }
    }

    public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly IBearerTokenService _tokenService;
        public ConfigureJwtBearerOptions(IBearerTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public void Configure(string name, JwtBearerOptions options)
        {

        }

        public void Configure(JwtBearerOptions options)
        {
            // default case: no scheme name was specified
            Configure(string.Empty, options);
        }
    }

    //public class SecurityRequirementsOperationFilter : IOperationFilter
    //{
    //    private readonly IOptions<AuthorizationOptions> authorizationOptions;

    //    public SecurityRequirementsOperationFilter(IOptions<AuthorizationOptions> authorizationOptions)
    //    {
    //        this.authorizationOptions = authorizationOptions;
    //    }

    //    public void Apply(Operation operation, OperationFilterContext context)
    //    {
    //        var controllerPolicies = context.ApiDescription.ControllerAttributes()
    //                                        .OfType<AuthorizeAttribute>()
    //                                        .Select(attr => attr.Policy);
    //        var actionPolicies = context.ApiDescription.ActionAttributes()
    //                                    .OfType<AuthorizeAttribute>()
    //                                    .Select(attr => attr.Policy);
    //        var policies = controllerPolicies.Union(actionPolicies).Distinct();
    //        var requiredClaimTypes = policies
    //            .Select(x => this.authorizationOptions.Value.GetPolicy(x))
    //            .SelectMany(x => x.Requirements)
    //            .OfType<ClaimsAuthorizationRequirement>()
    //            .Select(x => x.ClaimType);

    //        if (requiredClaimTypes.Any())
    //        {
    //            operation.Responses.Add("401", new Response { Description = "Unauthorized" });
    //            operation.Responses.Add("403", new Response { Description = "Forbidden" });

    //            operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
    //            operation.Security.Add(
    //                                   new Dictionary<string, IEnumerable<string>>
    //                                   {
    //                                       { "oauth2", requiredClaimTypes }
    //                                   });
    //        }
    //    }
    //}

}
