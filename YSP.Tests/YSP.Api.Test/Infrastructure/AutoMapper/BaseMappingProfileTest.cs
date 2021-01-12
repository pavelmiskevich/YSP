using System;
using AutoMapper;
using FluentAssertions;
using Xunit;
using YSP.Api.Mapping;
using YSP.Api.Resources;
using YSP.Core.Models;

namespace CustomerApi.Test.Infrastructure.AutoMapper
{
    public class BaseMappingProfileTest
    {
        protected readonly IMapper _mapper;

        public BaseMappingProfileTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }
    }
}