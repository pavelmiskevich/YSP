using System;
using AutoMapper;
using FluentAssertions;
using Xunit;
using YSP.Api.Mapping;
using YSP.Api.Resources;
using YSP.Core.Models;

namespace CustomerApi.Test.Infrastructure.AutoMapper
{
    public class MappingProfileTests
    {
        protected MapperConfiguration MockMapper { get; }
        protected IMapper Mapper { get; }

        public MappingProfileTests()
        {
            MockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            Mapper = MockMapper.CreateMapper();
        }

        [Fact]
        public void MappingProfile_ShouldHaveValidConfig()
        {
            MockMapper.AssertConfigurationIsValid();
        }
    }
}