using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YSP.Core;
using YSP.Core.Models;
using YSP.Core.Services;

namespace YSP.Services
{
    public class SystemStateService : ISystemStateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SystemStateService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<SystemState> CreateSystemState(SystemState newSystemState)
        {
            await _unitOfWork.SystemStates.AddAsync(newSystemState);
            await _unitOfWork.CommitAsync();
            return newSystemState;
        }

        public async Task DeleteSystemState(SystemState systemState)
        {
            _unitOfWork.SystemStates.Remove(systemState);
            await _unitOfWork.CommitAsync();
        }

        public async Task<SystemState> GetSystemStateById(int id)
        {
            return await _unitOfWork.SystemStates
                .GetByIdAsync(id);
        }

        public async Task UpdateSystemState(SystemState systemStateToBeUpdated, SystemState systemState)
        {
            systemStateToBeUpdated.Name = systemState.Name;
            systemStateToBeUpdated.Value = systemState.Value;
            systemStateToBeUpdated.IsActive = systemState.IsActive;

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<SystemState>> GetAllSystemStates()
        {
            return await _unitOfWork.SystemStates
                .GetAllAsync();
        }
    }
}
