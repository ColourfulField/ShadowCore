using ShadowBox.AutomaticDI.Interfaces;

namespace DotnetCoreAngularStarter.BusinessLogic.Services.Abstract
{
    public interface IDatabaseSeederService: ITransientLifetime
    {
        void EnsureDatabasesSeeded();
    }
}
