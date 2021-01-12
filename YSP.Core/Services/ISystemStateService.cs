using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Core.Models;

namespace YSP.Core.Services
{
    public interface ISystemStateService
    {
        Task<IEnumerable<SystemState>> GetAllSystemStates();
        Task<SystemState> GetSystemStateById(int id);
        Task<SystemState> CreateSystemState(SystemState newSystemState);
        Task UpdateSystemState(SystemState systemStateToBeUpdated, SystemState systemState);
        Task DeleteSystemState(SystemState systemState);
    }
}
