using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowTools.AutomaticDI
{
    [Obsolete("This is the class for basic .NET Core DI. It is obsolete now, use AutoRegistrationModule from Autofac instead.")]
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddShadowToolsDependencyInjection(this IServiceCollection services, IEnumerable<RuntimeLibrary> runtimeLibraries)
        {
            //containerBuilder.RegisterType<Model>().AsSelf().InstancePerRequest();
            //containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            var assemblies = new List<Assembly>();

            foreach (var library in runtimeLibraries)
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.ExportedTypes)
                {
                    if (type.IsAbstract == false && type.IsInterface == false && typeof(IInjectable).IsAssignableFrom(type))
                    {
                        var firstInterface = type.GetInterfaces().FirstOrDefault();
                        if (firstInterface == null)
                        {
                            continue;
                        }

                        ServiceDescriptor serviceDescriptor = null;
                        if (typeof(ISingletonLifetime).IsAssignableFrom(type))
                        {
                            serviceDescriptor = new ServiceDescriptor(firstInterface, type, ServiceLifetime.Singleton);
                        }
                        if (typeof(IScopedLifetime).IsAssignableFrom(type))
                        {
                            serviceDescriptor = new ServiceDescriptor(firstInterface, type, ServiceLifetime.Scoped);
                        }
                        if (typeof(ITransientLifetime).IsAssignableFrom(type))
                        {
                            serviceDescriptor = new ServiceDescriptor(firstInterface, type, ServiceLifetime.Transient);
                        }

                        if (serviceDescriptor == null)
                        {
                            continue;
                        }

                        services.Add(serviceDescriptor);
                    }
                }
            }
            return services;
        }
    }
}