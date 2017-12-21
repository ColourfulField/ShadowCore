using DotnetCoreAngularStarter.DAL.EntityFramework.Abstract;
using ShadowBox.Mapper.Abstract;

namespace DotnetCoreAngularStarter.BusinessLogic.Services.Abstract
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;

        protected BaseService(IUnitOfWork unitOfWork = null, IMapper mapper = null)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
    }
}
