using DotnetCoreAngularStarter.DAL.EntityFramework.Abstract;
using ShadowBox.Mapper.Abstract;

namespace DotnetCoreAngularStarter.BusinessLogic.Services.Abstract
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
