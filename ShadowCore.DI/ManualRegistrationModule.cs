using Autofac;
using ShadowCore.BusinessLogic.Services;
using ShadowCore.BusinessLogic.Services.Abstract;

namespace ShadowCore.DI
{
    public class ManualRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ManuallyRegisteredService>().As<IManuallyRegisteredService>().InstancePerDependency();
        }
    }
}
