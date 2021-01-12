using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YSP.Core;
using YSP.Core.Models;
using YSP.Core.Services;

namespace YSP.Services
{
    public class RegionService : IRegionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegionService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Region> CreateRegion(Region newRegion)
        {
            await _unitOfWork.Regions.AddAsync(newRegion);
            await _unitOfWork.CommitAsync();
            return newRegion;
        }

        public async Task DeleteRegion(Region region)
        {
            _unitOfWork.Regions.Remove(region);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Region> GetRegionById(int id)
        {
            return await _unitOfWork.Regions
                .GetByIdAsync(id);
        }

        public async Task UpdateRegion(Region regionToBeUpdated, Region region)
        {
            regionToBeUpdated.Name = region.Name;            
            regionToBeUpdated.IsActive = region.IsActive;

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Region>> GetAllRegions()
        {
            return await _unitOfWork.Regions.GetAllAsync();
        }
    }
}
