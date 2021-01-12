using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YSP.Core.DTO;
using YSP.Core.Models;
using YSP.Core.Services;
using YSP.Search.Core.Classes;
using YSP.Search.Core.Classes.Static;
using YSP.Search.Core.Structures;

namespace YSP.Operations
{
    public class ScheduleOperations
    {
        private readonly IScheduleService _scheduleService;
        private readonly IUserService _userService;
        private readonly IPositionService _positionService;
        private readonly ILogger<ScheduleOperations> _logger;
        //private readonly IMapper _mapper;

        public ScheduleOperations(IScheduleService scheduleService, IUserService userService
            , IPositionService positionService, ILogger<ScheduleOperations> logger, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _userService = userService;
            _positionService = positionService;
            _logger = logger;
            //_mapper = mapper;
        }        

        /// <summary>
        /// Check Queries positions in Schedule Without CancellationToken
        /// </summary>
        /// <returns>the success of the operation</returns>
        public async ValueTask<bool> Check()
        {
            return await Check(new CancellationToken());
        }

        /// <summary>
        /// Check Queries positions in Schedule With CancellationToken
        /// </summary>
        /// <returns>the success of the operation</returns>
        public async ValueTask<bool> Check(CancellationToken token)
        {
            IEnumerable<QueryRegionDTO> queryRegionDTOs = null;
            try
            {
                //обновить раписание на сегодня
                //await Update(token);
                await _scheduleService.UpdateToday(token);
                //получить список запросов для обработки
                int allLimit = await _userService.GetAllYandexLimit();
                int count = LimitConstraint.GetLimit(allLimit);
                queryRegionDTOs = await _scheduleService.GetAllTodayWithQueryByCount(count); // GetAllTodayWithQuery();
                //выполнить запросы к яндексу 
                var user = await _userService.GetUserById(1);
                var _DefaultCredential = new APICredentials() { User = user.YandexLogin, Key = user.YandexKey };
                //var _DefaultCredential = new APICredentials() { User = user.GoogleCx, Key = user.GoogleKey };
                foreach (var query in queryRegionDTOs)
                {
                    if (!token.IsCancellationRequested)
                    {
                        //var _query = new YandexSearchQuery(query, _DefaultCredential, token);
                        var _query = new GlobalSearch(new YandexSearch(), query, _DefaultCredential, token);
                        //var _query = new GlobalSearch(new GoogleSearch(), query, _DefaultCredential, token);
                        //TODO: подумать обойтись без этого преобразования!!!
                        var resultList = (await _query.GetResponseToList()).Cast<YandexSearchResult>().ToList();
                        //обработать ответ
                        var res = resultList.Find(s => s.Domain.Contains(query.Url));
                        //сохранить позиции в БД
                        //if (res.Position != 0)
                        //{
                            await _positionService.CreatePosition(new Position { QueryId = query.Id, Pos = res.Position });
                        //}
                        await _scheduleService.DeactivateScheduleAfterCheckById(query.ScheduleId);
                    }
                    else
                        _logger.LogWarning($"Операция ScheduleOperations Check прервана");
                }
                //обновить расписание на завтра
                await _scheduleService.UpdateFuture(token);
                //обновить системную статистику            
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error in Check(token) {ex.Message}");
                return false;
            }
            //return queryRegionDTOs;
            return true;
        }

        /// <summary>
        /// Check Queriy position in Schedule Without CancellationToken
        /// </summary>
        /// <returns>the success of the operation</returns>
        public async ValueTask<bool> CheckSingle(QueryRegionDTO queryRegionDTO)
        {
            return await CheckSingle(queryRegionDTO, new CancellationToken());
        }

        /// <summary>
        /// Check Queriy position in Schedule With CancellationToken
        /// </summary>
        /// <returns>the success of the operation</returns>
        public async ValueTask<bool> CheckSingle(QueryRegionDTO queryRegionDTO, CancellationToken token)
        {
            try
            {                
                //выполнить запросы к яндексу 
                var user = await _userService.GetUserById(1);
                //var _DefaultCredential = new APICredentials() { User = user.YandexLogin, Key = user.YandexKey };
                var _DefaultCredential = new APICredentials() { User = user.GoogleCx, Key = user.GoogleKey };

                //TODO: сделать проверку на лимит в текущем часу и возвращать false в случае превышения для проверки в следующем часу
                if (!token.IsCancellationRequested)
                {
                    //var _query = new YandexSearchQuery(query, _DefaultCredential, token);
                    //var _query = new GlobalSearch(new YandexSearch(), queryRegionDTO, _DefaultCredential, token);
                    var _query = new GlobalSearch(new GoogleSearch(), queryRegionDTO, _DefaultCredential, token);
                    //TODO: подумать обойтись без этого преобразования!!!
                    var resultList = (await _query.GetResponseToList()).Cast<YandexSearchResult>().ToList();
                    //обработать ответ
                    var res = resultList.Find(s => s.Domain.Contains(queryRegionDTO.Url));
                    //сохранить позиции в БД
                    //if (res.Position != 0)
                    //{
                    await _positionService.CreatePosition(new Position { QueryId = queryRegionDTO.Id, Pos = res.Position });
                    //}
                    await _scheduleService.DeactivateScheduleAfterCheckById(queryRegionDTO.ScheduleId);
                }
                else
                    _logger.LogWarning($"Операция ScheduleOperations CheckSingle прервана");
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Error in CheckSingle(token) {ex.Message}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Update Schedule Without CancellationToken
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private async Task Update()
        {
            await Update(new CancellationToken());
        }

        //https://habr.com/ru/post/168669/
        /// <summary>
        /// Update Schedule With CancellationToken
        /// </summary>
        /// <param name="token">CancellationToken</param>
        /// <returns>Task</returns>
        [Obsolete]
        private async Task Update(CancellationToken token)
        {
#if DEBUG
            int sCount = await _scheduleService.GetCountToday();
#endif
            await _scheduleService.UpdateToday(token);
#if DEBUG
            sCount = await _scheduleService.GetCountToday();
#endif
        }
    }
}
