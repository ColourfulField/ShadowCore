using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using ShadowBox.AutomaticDI.Interfaces;

namespace ShadowBox.AutomaticDI
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddShadowBoxDependencyInjection(this IServiceCollection services, IEnumerable<RuntimeLibrary> runtimeLibraries)
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