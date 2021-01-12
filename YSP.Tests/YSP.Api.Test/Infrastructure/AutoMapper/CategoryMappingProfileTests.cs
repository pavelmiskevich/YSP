using System;
using AutoMapper;
using FluentAssertions;
using Xunit;
using YSP.Api.Mapping;
using YSP.Api.Resources;
using YSP.Core.Models;

namespace CustomerApi.Test.Infrastructure.AutoMapper
{
    public class CategoryMappingProfileTests : MappingProfileTests
    {
        private readonly CategoryResource _categoryResource;
        private readonly SaveCategoryResource _saveCategoryResource;

        public CategoryMappingProfileTests()
        {
            _categoryResource = new CategoryResource
            {
                Name = "CategoryName",
                Parent = new CategoryResource { Name = "CategoryParentName" }
            };
            _saveCategoryResource = new SaveCategoryResource
            {
                Name = "CategoryName",
                ParentId = 1
            };
        }

        [Fact]
        public void Map_Category_CategoryResource_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Category, CategoryResource>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_Category_SaveCategoryResource_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Category, SaveCategoryResource>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_CategoryResource_Category_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<CategoryResource, Category>()
                    .ForMember(x => x.InverseParent, opt => opt.Ignore())
                    .ForMember(x => x.TimeStamp, opt => opt.Ignore())
                    .ForMember(x => x.Sites, opt => opt.Ignore())
            );
            
            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_SaveCategoryResource_Category_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<SaveCategoryResource, Category>()
                    .ForMember(x => x.Id, opt => opt.Ignore())
                    .ForMember(x => x.AddDate, opt => opt.Ignore())
                    .ForMember(x => x.Parent, opt => opt.Ignore())
                    .ForMember(x => x.InverseParent, opt => opt.Ignore())
                    .ForMember(x => x.TimeStamp, opt => opt.Ignore())
                    .ForMember(x => x.Sites, opt => opt.Ignore())                    
                );

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_Category_Category_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Category, Category>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_CategoryResource_Category()
        {
            var category = Mapper.Map<Category>(_categoryResource);

            category.Name.Should().Be(_categoryResource.Name);
            category.Parent.Name.Should().Be(_categoryResource.Parent.Name);
        }

        [Fact]
        public void Map_SaveCategoryResource_Customer()
        {
            var category = Mapper.Map<Category>(_saveCategoryResource);

            category.Name.Should().Be(_saveCategoryResource.Name);
            category.ParentId.Should().Be(_saveCategoryResource.ParentId);
        }
    }
}