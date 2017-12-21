using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Microsoft.Extensions.DependencyModel;
using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowTools.AutomaticDI
{
    public class AutoRegistrationModule : Autofac.Module
    {
        private readonly IEnumerable<RuntimeLibrary> _runtimeLibraries;

        public AutoRegistrationModule(IEnumerable<RuntimeLibrary> runtimeLibraries)
        {
            _runtimeLibraries = runtimeLibraries;
        }

        protected override void Load(ContainerBuilder builder)
        {
            foreach (var library in _runtimeLibraries)
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                foreach (var type in assembly.ExportedTypes)
                {
                    var firstImplementedInterface = type.GetInterfaces().FirstOrDefault();
                    if (!TypeIsEligibleForInjection(type, firstImplementedInterface))
                    {
                        continue;
                    }

                    var registration = builder.RegisterType(type).AsSelf();
                    var interfaceTypes = type.GetInterfaces().Where(x => x.IsAssignableTo<IInjectable>()).ToList();

                    foreach (var interfaceType in interfaceTypes)
                    {
                        registration = registration.As(interfaceType);
                        ApplyLifetimeScope(registration, interfaceType);
                    }
                }
            }
        }

        private bool TypeIsEligibleForInjection(Type type, Type firstImplementedInterface)
        {
            return type.IsAbstract == false
                && type.IsInterface == false
                && typeof(IInjectable).IsAssignableFrom(type)
                && firstImplementedInterface != null;
        }

        private void ApplyLifetimeScope(IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registration, Type serviceType)
        {
            if (serviceType.IsAssignableTo<ISingletonLifetime>())
            {
                registration.SingleInstance();
            }
            else
            {
                if (serviceType.IsAssignableTo<IScopedLifetime>())
                {
                    registration.InstancePerLifetimeScope();
                }
                else
                {
                    if (serviceType.IsAssignableTo<ITransientLifetime>())
                    {
                        registration.InstancePerDependency();
                    }
                }
            }
        }
    }
}
