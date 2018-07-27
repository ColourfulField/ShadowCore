using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using ShadowTools.AutomaticDI.Interfaces;
using ShadowTools.Utilities.Extensions.Reflection;

namespace ShadowTools.AutomaticDI
{
    public static class AutomaticDiExtensions
    {
        public static IServiceCollection AddShadowToolsAutomaticDi(this IServiceCollection services, IEnumerable<RuntimeLibrary> runtimeLibraries)
        {
            foreach (var library in runtimeLibraries)
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                foreach (var type in assembly.ExportedTypes)
                {
                    if (!TypeIsEligibleForInjection(type))
                    {
                        continue;
                    }

                    var interfaces = type.GetInterfaces().Where(x => x.IsAssignableTo<IInjectable>());

                    //Register type as self
                    var selfDescriptor = type.GetDesriptor();
                    selfDescriptor = ApplyLifetimeScope(selfDescriptor);
                    services.Add(selfDescriptor);

                    //Register type as his implemented interfaces
                    foreach (var interfaceType in interfaces)
                    {
                        var descriptor = type.GetDesriptor()
                                             .As(interfaceType)
                                             .ApplyLifetimeScope();

                        services.Add(descriptor);
                    }
                }
            }
            return services;
        }

        private static bool TypeIsEligibleForInjection(Type type)
        {
            return type.IsAbstract == false
                   && type.IsInterface == false
                   && typeof(IInjectable).IsAssignableFrom(type)
                   && type.GetInterfaces().Any();
        }

        private static ServiceDescriptor ApplyLifetimeScope(this ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationType.IsAssignableTo<ISingletonLifetime>())
            {
                return descriptor.SingletonLifetime();
            }

            if (descriptor.ImplementationType.IsAssignableTo<IScopedLifetime>())
            {
                return descriptor.ScopedLifetime();
            }

            if (descriptor.ImplementationType.IsAssignableTo<ITransientLifetime>())
            {
                return descriptor.TransientLifetime();
            }

            throw new ArgumentException($"Automatic DI configuration error: Type implements {nameof(IInjectable)}, but doesn't" +
                                        $" implement any of the lifetime interfaces. Make type implement {nameof(ITransientLifetime)}," +
                                        $" {nameof(IScopedLifetime)} or {nameof(ISingletonLifetime)} instead of {nameof(IInjectable)}.");
        }


        private static ServiceDescriptor GetDesriptor(this Type type)
        {
            return new ServiceDescriptor(type, type, ServiceLifetime.Transient);
        }

        private static ServiceDescriptor As(this ServiceDescriptor descriptor, Type serviceType)
        {
            if (descriptor.ImplementationFactory != null)
            {
                return new ServiceDescriptor(serviceType, descriptor.ImplementationFactory, ServiceLifetime.Transient);
            }

            return new ServiceDescriptor(serviceType, descriptor.ImplementationType, ServiceLifetime.Transient);
        }

        private static ServiceDescriptor WithLifetime(this ServiceDescriptor descriptor, ServiceLifetime lifetime)
        {
            if (descriptor.ImplementationFactory != null)
            {
                return new ServiceDescriptor(descriptor.ServiceType, descriptor.ImplementationFactory, lifetime);
            }

            return new ServiceDescriptor(descriptor.ServiceType, descriptor.ImplementationType, lifetime);
        }

        private static ServiceDescriptor TransientLifetime(this ServiceDescriptor descriptor)
        {
            return descriptor.WithLifetime(ServiceLifetime.Transient);
        }

        private static ServiceDescriptor ScopedLifetime(this ServiceDescriptor descriptor)
        {
            return descriptor.WithLifetime(ServiceLifetime.Scoped);
        }

        private static ServiceDescriptor SingletonLifetime(this ServiceDescriptor descriptor)
        {
            return descriptor.WithLifetime(ServiceLifetime.Singleton);
        }
    }
}
