using System;
using System.Threading.Tasks;
using YSP.Core.Repositories;

namespace YSP.Core
{
    public interface IUnitOfWork : IDisposable
    {   
        ICategoryRepository Categories { get; }
        IFeedbackRepository Feedbacks { get; }
        IOfferRepository Offers { get; }
        IPositionRepository Positions { get; }
        IQueryRepository Queries { get; }
        IRegionRepository Regions { get; }
        IScheduleRepository Schedules { get; }
        ISiteRepository Sites { get; }
        ISystemStateRepository SystemStates { get; }
        IUserRepository Users { get; }
        
        Task<int> CommitAsync();
    }
}
