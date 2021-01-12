using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YSP.Core;
using YSP.Core.Models;
using YSP.Core.Services;

namespace YSP.Services
{
    public class SiteService : ISiteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SiteService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Site> CreateSite(Site newSite)
        {
            await _unitOfWork.Sites.AddAsync(newSite);
            await _unitOfWork.CommitAsync();
            return newSite;
        }

        public async Task DeleteSite(Site site)
        {
            _unitOfWork.Sites.Remove(site);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Site> GetSiteById(int id)
        {
            return await _unitOfWork.Sites
                .GetByIdAsync(id);
        }

        public async Task UpdateSite(Site siteToBeUpdated, Site site)
        {
            siteToBeUpdated.Name = site.Name;
            siteToBeUpdated.Url = site.Url;
            siteToBeUpdated.Descr = site.Descr;
            siteToBeUpdated.LastCheck = site.LastCheck;
            siteToBeUpdated.TimeOut = site.TimeOut;
            siteToBeUpdated.UserId = site.UserId;
            siteToBeUpdated.CategoryId = site.CategoryId;
            siteToBeUpdated.RegionId = site.RegionId;
            siteToBeUpdated.IsActive = site.IsActive;

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Site>> GetAllWithReference()
        {
            return await _unitOfWork.Sites
                .GetAllWithReferenceAsync();
        }

        public async Task<IEnumerable<Site>> GetAllWithCategory()
        {
            return await _unitOfWork.Sites
                .GetAllWithCategoryAsync();
        }

        public async Task<IEnumerable<Site>> GetAllWithRegion()
        {
            return await _unitOfWork.Sites
                .GetAllWithRegionAsync();
        }

        public async Task<IEnumerable<Site>> GetAllWithUser()
        {
            return await _unitOfWork.Sites
                .GetAllWithUserAsync();
        }
        //TODO: запутался, нужно ли здесь получение сайтов с категориями
        public async Task<IEnumerable<Site>> GetSitesByCategoryId(int categoryId)
        {
            return await _unitOfWork.Sites
                .GetAllWithCategoryByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Site>> GetSitesByRegionId(int regionId)
        {
            return await _unitOfWork.Sites
                .GetAllWithRegionByRegionIdAsync(regionId);
        }

        public async Task<IEnumerable<Site>> GetSitesByUserId(int userId)
        {
            return await _unitOfWork.Sites
                .GetAllWithUserByUserIdAsync(userId);
        }
    }
}
