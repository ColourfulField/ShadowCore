using Autofac;
using DotnetCoreAngularStarter.BusinessLogic.Services;
using DotnetCoreAngularStarter.BusinessLogic.Services.Abstract;

namespace DotnetCoreAngularStarter.DI
{
    public class ManualRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ManuallyRegisteredService>().As<IManuallyRegisteredService>().InstancePerDependency();
        }
    }
}
