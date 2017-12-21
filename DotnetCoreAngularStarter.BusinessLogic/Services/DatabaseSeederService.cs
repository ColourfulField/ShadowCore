using DotnetCoreAngularStarter.BusinessLogic.Services.Abstract;
using DotnetCoreAngularStarter.DAL.EntityFramework.Abstract;

namespace DotnetCoreAngularStarter.BusinessLogic.Services
{
    public class DatabaseSeederService: IDatabaseSeederService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DatabaseSeederService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void EnsureDatabasesSeeded()
        {
            EnsureSqlSeeded();
        }

        private void EnsureSqlSeeded()
        {
            _unitOfWork.EnsureDatabaseSeeded();
        }
    }
}
