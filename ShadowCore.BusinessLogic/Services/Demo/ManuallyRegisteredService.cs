using ShadowCore.BusinessLogic.Services.Abstract;

namespace ShadowCore.BusinessLogic.Services
{
    public class ManuallyRegisteredService : IManuallyRegisteredService
    {
        public string GetValue()
        {
            return "hello from manually registered service";
        }
    }
}
