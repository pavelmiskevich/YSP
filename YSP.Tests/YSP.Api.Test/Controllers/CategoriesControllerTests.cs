using System;
using Xunit;
using YSP.Api.Controllers;
using YSP.Api.Resources;
using YSP.Core.Services;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FakeItEasy;
using FluentAssertions;
using AutoMapper;
using YSP.Core.Models;
using Microsoft.Extensions.Logging;
using YSP.Api.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace YSP.Api.Test
{
    public class CategoriesControllerTests
    {
        //Для MVC
        //https://metanit.com/sharp/aspnet5/22.5.php
        //Для WebAPI
        //https://metanit.com/sharp/aspnet5/23.3.php
        //Invoke-RestMethod http://localhost:5000/api/Categories -Method GET
        //Invoke-RestMethod https://localhost:5001/api/Categories -Method GET
        //POST
        //Invoke-RestMethod http://localhost:5000/api/Categories -Method POST -Body (@{name = "Тестовая категория1"} | ConvertTo-Json) -ContentType "application/json; charset=utf-8"
        //Invoke-RestMethod https://localhost:5001/api/Categories -Method POST -Body (@{name = "Тестовая категория2"} | ConvertTo-Json) -ContentType "application/json; charset=utf-8"
        //Invoke-RestMethod http://localhost:5000/api/Categories -Method POST -Body (@{name = "Тестовая категория3"; parentId = 3} | ConvertTo-Json) -ContentType "application/json; charset=utf-8"
        //Invoke-RestMethod https://localhost:5001/api/Categories -Method POST -Body (@{name = "Тестовая категория4"; parentId = 3} | ConvertTo-Json) -ContentType "application/json; charset=utf-8"
        //PUT
        //Invoke-RestMethod http://localhost:5000/api/Categories/13 -Method PUT -Body (@{id = 13; name = "Изменение 1"; parentId = 3} | ConvertTo-Json) -ContentType "application/json"
        //Invoke-RestMethod https://localhost:5001/api/Categories/15 -Method PUT -Body (@{name = "Изменение 2";} | ConvertTo-Json) -ContentType "application/json; charset=utf-8"
        //DELETE
        //Invoke-RestMethod http://localhost:5000/api/Categories/13 -Method DELETE
        //Invoke-RestMethod https://localhost:5001/api/Categories/14 -Method DELETE

        //https://stackoverflow.com/questions/51239221/unit-testing-asp-net-core-web-api-using-xunit-and-moq
        //public Mock<IHostingEnvironment> HostingEnvironment { get; set; }
        //private readonly Mock<ILogger<SubjectController>> _logger;
        //private readonly Mock<ISubjectManager<Subject>> _subjectManager;
        //private readonly Mock<IChapterManager<Chapter>> _chapterManager;
        //private readonly Mock<ITopicManager<Topic>> _topicManager;
        //private readonly Mock<ILearningPointManager<LearningPoint>> _learningPointManager;
        //private SubjectController controller;  

        private readonly CategoriesController _categoriesController;
        private readonly Category _categoryModel;
        private readonly SaveCategoryResource _saveCategoryResource;
        private readonly SaveCategoryResource _updateCategoryResource;
        private readonly int _id = 0;

        public CategoriesControllerTests()
        {
            //var mapper = A.Fake<IMapper>();
            var mappingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            IMapper mapper = new Mapper(configuration);

            var logger = A.Fake<ILogger<CategoriesController>>();
            var categoryService = A.Fake<ICategoryService>();
            _categoriesController = new CategoriesController(categoryService, mapper, logger);            

            var category = new Category
            {
                //Id = _id,
                Name = "CategoryName1",
                ParentId = 1
            };
            _categoryModel = new Category
            {
                Name = "CategoryName2",
                ParentId = 2
            };
            _saveCategoryResource = new SaveCategoryResource
            {
                Name = "CategoryNameSave",
                ParentId = 3
            };
            _updateCategoryResource = new SaveCategoryResource
            {
                Name = "CategoryNameUpdate",
                ParentId = 4
            };

            //A.CallTo(() => mapper.Map<Category>(A<Category>._)).Returns(category);
            A.CallTo(() => categoryService.GetAllWithParentCategory()).Returns(GetAllWithParentCategory());
            //A.CallTo(() => _mediator.Send(A<CreateCustomerCommand>._, default)).Returns(customer);
            //A.CallTo(() => _mediator.Send(A<UpdateCustomerCommand>._, default)).Returns(customer);
        }

        [Fact]
        public async Task GetCategoriesTestAsync()
        {
            var result = await _categoriesController.GetCategories();
            var resultValue = result.Value as IEnumerable<CategoryResource>;

            result.Result.Should().NotBeNull();
            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (resultValue as List<CategoryResource>)?.Count.Should().Be((await GetAllWithParentCategory()).Count());            
        }

        [Theory]
        [InlineData("CreateCustomerAsync: customer is null")]
        public async void Post_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            //A.CallTo(() => _mediator.Send(A<CreateCustomerCommand>._, default)).Throws(new ArgumentException(exceptionMessage));

            var result = await _categoriesController.CreateCategory(_saveCategoryResource);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        //[Theory]
        //[InlineData("UpdateCustomerAsync: customer is null")]
        //[InlineData("No user with this id found")]
        //public async void Put_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        //{
        //    //A.CallTo(() => _mediator.Send(A<UpdateCustomerCommand>._, default)).Throws(new Exception(exceptionMessage));

        //    var result = await _categoriesController.UpdateCategory(_id, _updateCategoryResource);

        //    (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        //    (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        //}

        [Fact]
        public async void Post_ShouldReturnCategory()
        {
            var result = await _categoriesController.CreateCategory(_saveCategoryResource);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            //result.Value.Should().BeOfType<Category>();
            //result.Value.Id.Should().Be(_id);
        }

        //[Fact]
        //public async void Put_ShouldReturnCategory()
        //{
        //    var result = await _categoriesController.UpdateCategory(3, _updateCategoryResource);

        //    (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        //    //result.Value.Should().BeOfType<Category>();
        //    //result.Value.Id.Should().Be(_id);
        //}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async Task<IEnumerable<Category>> GetAllWithParentCategory()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Category1" },
                new Category { Id = 2, Name = "Category2", Parent = new Category { Id = 1, Name = "ParentCategory1"} },
                new Category { Id = 3, Name = "Category3" }
            };
            return categories;
        }
    }
}
