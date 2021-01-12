using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Repositories
{
    //TODO: подумать над обобщенным репозиторием для простых сущностей типа Отзыва или Предложения
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        Task<IEnumerable<Feedback>> GetAllWithUserAsync();
        Task<Feedback> GetWithUserByIdAsync(int id);
        Task<IEnumerable<Feedback>> GetAllWithUserByUserIdAsync(int userId);
    }
}
