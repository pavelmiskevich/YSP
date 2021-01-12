using AutoMapper;
using YSP.Core.DTO;
using YSP.Core.Models;

namespace Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Schedule, QueryRegionDTO>()
                .ForMember("Id", opt => opt.MapFrom(c => c.QueryId))
                .ForMember("ScheduleId", opt => opt.MapFrom(c => c.Id))
                .ForMember("QueryName", opt => opt.MapFrom(c => c.Query.Name))
                .ForMember("Region", opt => opt.MapFrom(c => c.Query.Site.Region))
                .ForMember("Url", opt => opt.MapFrom(c => c.Query.Site.Url));
            //CreateMap<Artist, ArtistResource>();

            // Resource to Domain
            CreateMap<QueryRegionDTO, Schedule>();
        }
    }
}
