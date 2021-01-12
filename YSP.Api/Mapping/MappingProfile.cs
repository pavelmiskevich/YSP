using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSP.Api.Resources;
using YSP.Core.Models;

namespace YSP.Api.Mapping
{
    //TODO: возможно стоит объединить маппинг в отдельную библиотеку или объедить с другими
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Category, CategoryResource>();
            CreateMap<Feedback, FeedbackResource>();
            CreateMap<Offer, OfferResource>();            
            CreateMap<Position, PositionResource>();
            CreateMap<Query, QueryResource>();
            CreateMap<Region, RegionResource>();
            CreateMap<Schedule, ScheduleResource>();
            CreateMap<Site, SiteResource>();
            CreateMap<SystemState, SystemStateResource>();
            CreateMap<User, UserResource>();

            // Resource to Domain
            CreateMap<CategoryResource, Category>()
                .ForMember(x => x.InverseParent, opt => opt.Ignore())
                .ForMember(x => x.TimeStamp, opt => opt.Ignore())
                .ForMember(x => x.Sites, opt => opt.Ignore());
            CreateMap<SaveCategoryResource, Category>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.AddDate, opt => opt.Ignore())
                .ForMember(x => x.Parent, opt => opt.Ignore())
                .ForMember(x => x.InverseParent, opt => opt.Ignore())
                .ForMember(x => x.TimeStamp, opt => opt.Ignore())
                .ForMember(x => x.Sites, opt => opt.Ignore());
            CreateMap<FeedbackResource, Feedback>();
            CreateMap<SaveFeedbackResource, Feedback>();
            CreateMap<OfferResource, Offer>();
            CreateMap<SaveOfferResource, Offer>();
            CreateMap<PositionResource, Position>();
            //CreateMap<SavePositionResource, Position>();
            CreateMap<QueryResource, Query>();
            CreateMap<SaveQueryResource, Query>();
            CreateMap<RegionResource, Region>();
            CreateMap<SaveRegionResource, Region>();
            CreateMap<ScheduleResource, Schedule>();
            //CreateMap<SaveScheduleResource, Schedule>();
            CreateMap<SiteResource, Site>();
            CreateMap<SaveSiteResource, Site>();
            CreateMap<SystemStateResource, SystemState>();
            //CreateMap<SaveSystemStateResource, SystemState>();
            CreateMap<UserResource, User>();
            CreateMap<SaveUserResource, User>();
        }
    }
}
