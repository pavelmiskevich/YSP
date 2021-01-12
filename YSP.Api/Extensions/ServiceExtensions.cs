using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using YSP.Core.Services;
using YSP.Services;

namespace YSP.Api.Extensions
{
    //TODO: возможно стоит вынести расширение в отдельную библиотеку
    public static class ServiceExtensions
    {
        //http://flash2048.com/post/asp-net-core-dependency-injection
        public static void AddYSPServices(this IServiceCollection services)
        {
            //TODO: проверить с другими :
            //Transient — Objects are different.One new instance is provided to every controller and every service
            //Scoped — Objects are same through the request
            //Singleton — Objects are the same for every request during the application lifetime
            services.TryAddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IFeedbackService, FeedbackService>();
            services.AddTransient<IOfferService, OfferService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<IQueryService, QueryService>();
            services.AddTransient<IRegionService, RegionService>();
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<ISiteService, SiteService>();
            services.AddTransient<ISystemStateService, SystemStateService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
