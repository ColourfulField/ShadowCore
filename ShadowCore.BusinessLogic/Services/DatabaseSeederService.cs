using ShadowCore.BusinessLogic.Services.Abstract;
using ShadowCore.DAL.EntityFramework.Abstract;

namespace ShadowCore.BusinessLogic.Services
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
