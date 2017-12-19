using DotnetCoreAngularStarter.BusinessLogic.Services.Abstract;

namespace DotnetCoreAngularStarter.BusinessLogic.Services
{
    public class ManuallyRegisteredService : IManuallyRegisteredService
    {
        public string GetValue()
        {
            return "hello from manually registered service";
        }
    }
}
