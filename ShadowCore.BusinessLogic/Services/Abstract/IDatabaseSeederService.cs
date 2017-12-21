using ShadowTools.AutomaticDI.Interfaces;

namespace ShadowCore.BusinessLogic.Services.Abstract
{
    public interface IDatabaseSeederService: ITransientLifetime
    {
        void EnsureDatabasesSeeded();
    }
}
