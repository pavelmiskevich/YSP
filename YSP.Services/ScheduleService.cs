using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YSP.Core;
using YSP.Core.DTO;
using YSP.Core.Models;
using YSP.Core.Repositories;
using YSP.Core.Services;

namespace YSP.Services
{
    public partial class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ScheduleService> _logger;
        private readonly IMapper _mapper;

        //public ScheduleService(IUnitOfWork unitOfWork)
        //{
        //    this._unitOfWork = unitOfWork;
        //}

        public ScheduleService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ScheduleService> logger) //: this(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._logger = logger; //loggerFactory.CreateLogger<ScheduleService>();
        }

        public async Task<IEnumerable<Schedule>> GetAllWithQuery()
        {
            return await _unitOfWork.Schedules
                .GetAllWithQueryAsync();
        }

        public async Task<Schedule> GetScheduleWithQueryById(int id)
        {
            return await _unitOfWork.Schedules
                .GetWithQueryByIdAsync(id);
        }

        public async Task<IEnumerable<Schedule>> GetSchedulesByQueryId(int queryId)
        {
            return await _unitOfWork.Schedules
                .GetAllWithQueryByQueryIdAsync(queryId);
        }

        public async Task<Schedule> CreateSchedule(Schedule newSchedule)
        {
            await _unitOfWork.Schedules.AddAsync(newSchedule);
            await _unitOfWork.CommitAsync();
            return newSchedule;
        }

        public async Task DeleteSchedule(Schedule schedule)
        {
            _unitOfWork.Schedules.Remove(schedule);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Schedule> GetScheduleById(int id)
        {
            return await _unitOfWork.Schedules
                .GetByIdAsync(id);
        }

        public async Task UpdateSchedule(Schedule scheduleToBeUpdated, Schedule schedule)
        {
            scheduleToBeUpdated.Date = schedule.Date;
            scheduleToBeUpdated.QueryId = schedule.QueryId;
            scheduleToBeUpdated.IsActive = schedule.IsActive;

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<QueryRegionDTO>> GetAllTodayWithQuery()
        {
            return await GetAllWithQueryByDate(DateTime.Now.Date);
        }
        public async Task<IEnumerable<QueryRegionDTO>> GetAllWithQueryByDate(DateTime date)
        {
            //https://metanit.com/sharp/mvc5/23.4.php
            // Настройка конфигурации AutoMapper
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Schedule, QueryRegionDTO>()
            //    .ForMember("Id", opt => opt.MapFrom(c => c.QueryId))
            //    .ForMember("Name", opt => opt.MapFrom(c => c.Query.Name))
            //    .ForMember("Region", opt => opt.MapFrom(c => c.Query.Site.Region)));
            //var mapper = new Mapper(config);

            //var schedules = await _unitOfWork.Schedules.GetAllWithQueryByDateAsync(date);
            IEnumerable<QueryRegionDTO> qrDTO = null;
            var schedules = await _unitOfWork.Schedules.GetAllTodayWithQuerySiteRegionAsync(date);
            try
            {
                //IEnumerable<QueryRegionDTO> qrDTO = _mapper.Map<IEnumerable<Schedule>, IEnumerable<QueryRegionDTO>>(schedules);
#warning так делать нельзя, так как регионы у всех разные
                //Query query = await _unitOfWork.Queries.GetWithSiteByIdAsync(qrDTO.First().Id);                
                //Site site = await _unitOfWork.Sites.GetWithRegionByIdAsync(query.SiteId);
                //qrDTO.ToList<QueryRegionDTO>().ForEach(x => x.Region = site.Region);

                qrDTO = _mapper.Map<IEnumerable<Schedule>, IEnumerable<QueryRegionDTO>>(schedules);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error GetAllTodayWithQueryByCount {ex.Message}");
            }
            return qrDTO;
            //return await _unitOfWork.Schedules
            //    .GetAllWithQueryByDateAsync(date);
        }
        public async Task<IEnumerable<QueryRegionDTO>> GetAllTodayWithQueryByCount(int count)
        {
            return await GetAllWithQueryByDateAndCount(DateTime.Now.Date, count);
        }
        public async Task<IEnumerable<QueryRegionDTO>> GetAllWithQueryByDateAndCount(DateTime date, int count)
        {
            IEnumerable<QueryRegionDTO> qrDTO = null;
            var schedules = await _unitOfWork.Schedules.GetAllTodayWithQuerySiteRegionByCountAsync(date, count);
            try
            {
                qrDTO = _mapper.Map<IEnumerable<Schedule>, IEnumerable<QueryRegionDTO>>(schedules);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error GetAllTodayWithQueryByCount {ex.Message}");
            }
            return qrDTO;
            //return await _unitOfWork.Schedules
            //    .GetAllWithQueryByDateAsync(date);
        }
    }
}
