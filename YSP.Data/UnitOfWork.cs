using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using YSP.Core;
using YSP.Core.Repositories;
using YSP.Data.Repositories;

namespace YSP.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly YSPDbContext _context;
        private CategoryRepository _categoryRepository;
        private FeedbackRepository _feedbackRepository;
        private OfferRepository _offerRepository;
        private PositionRepository _positionRepository;
        private QueryRepository _queryRepository;
        private RegionRepository _regionRepository;
        private ScheduleRepository _scheduleRepository;
        private SiteRepository _siteRepository;
        private SystemStateRepository _systemStateRepository;
        private UserRepository _userRepository;

        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(YSPDbContext context)
        {
            this._context = context;
        }

        public UnitOfWork(YSPDbContext context, ILoggerFactory loggerFactory) : this(context)
        {
            _logger = loggerFactory.CreateLogger<UnitOfWork>();
        }

        public ICategoryRepository Categories => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

        public IFeedbackRepository Feedbacks => _feedbackRepository ??= new FeedbackRepository(_context);

        public IOfferRepository Offers => _offerRepository ??= new OfferRepository(_context);

        public IPositionRepository Positions => _positionRepository ??= new PositionRepository(_context);

        public IQueryRepository Queries => _queryRepository ??= new QueryRepository(_context);

        public IRegionRepository Regions => _regionRepository ??= new RegionRepository(_context);

        public IScheduleRepository Schedules => _scheduleRepository ??= new ScheduleRepository(_context);

        public ISiteRepository Sites => _siteRepository ??= new SiteRepository(_context);

        public ISystemStateRepository SystemStates => _systemStateRepository ??= new SystemStateRepository(_context);

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        //TODO: проверить логирование ошибок
        //TODO: сделать отправу ошибко в Slack
        public async Task<int> CommitAsync()
        {            
            try
            {                
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Генерируется, когда возникла ошибка, связанная с параллелизмом.
                // Пока что просто сгенерировать исключение повторно, 
                _logger.LogError($"Ошибка параллелизма: " + ex.Message);
                throw ex;                
            }
            catch (RetryLimitExceededException ex)
            {
                // Генерируется, когда достигнуто максимальное количество попыток.
                // Дополнительные детали можно найти во внутреннем исключении (исключениях) . 
                // Пока что просто сгенерировать исключение повторно.
                _logger.LogError($"Достигнуто максимальное количество попыток: " + ex.Message);
                throw ex;
            }
            catch (DbUpdateException ex)
            {
                // Генерируется, когда обновление базы данных потерпело неудачу.
                // Дополнительные детали и затронутые объекты можно 
                // найти во внутреннем исключении (исключениях).
                // Пока что просто сгенерировать исключение повторно, 
                _logger.LogError($"Обновление базы данных потерпело неудачу: " + ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                // Возникло какое-то другое исключение, которое должно быть обработано, 
                _logger.LogError($"Возникло какое-то другое исключение: " + ex.Message);
                throw ex;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
