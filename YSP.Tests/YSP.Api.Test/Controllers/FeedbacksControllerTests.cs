using Xunit;
using YSP.Api.Controllers;
using YSP.Api.Resources;
using YSP.Core.Services;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using AutoMapper;
using YSP.Core.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using YSP.Api.Mapping;
using System.Linq;

namespace YSP.Api.Test
{
    public class FeedbacksControllerTests
    {
        //https://metanit.com/sharp/aspnet5/22.4.php
        //https://stackoverflow.com/questions/51239221/unit-testing-asp-net-core-web-api-using-xunit-and-moq
        //https://habr.com/ru/post/150859/

        private readonly FeedbacksController _feedbacksController;

        public FeedbacksControllerTests()
        {
            //var mapper = A.Fake<IMapper>();
            //var logger = A.Fake<ILogger>();
            //var ICategoryService = A.Fake<ICategoryService>();
            var feedbackService = new Mock<IFeedbackService>();
            //var mapper = new Mock<IMapper>();
            //mapper.Setup(m => m.Map<IEnumerable<Feedback>, IEnumerable<FeedbackResource>>(It.IsAny<IEnumerable<Feedback>>())).Returns(new IEnumerable<FeedbackResource>());

            var mappingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            IMapper mapper = new Mapper(configuration);

            feedbackService.Setup(repo => repo.GetAllWithUser()).Returns(GetAllWithUser());

            _feedbacksController = new FeedbacksController(feedbackService.Object, mapper);            
        }

        [Fact]
        public async void GetCategoriesTest()
        {
            var actionResult = await _feedbacksController.GetFeedbacks();
            var result = actionResult.Result as OkObjectResult;
            var resultValue = result.Value as IEnumerable<FeedbackResource>;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            resultValue.Count().Should().Be((await GetAllWithUser()).Count());

            // Мы не передаем методу Verify никаких дополнительных параметров.
            // Это значит, что будут использоваться ожидания установленные
            // с помощью mock.Setup
            //mock.Verify();
        }

        //TODO: сделать для Get по Id
        //[Theory]
        //[InlineData("CreateCustomerAsync: customer is null")]
        //public async void Post_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        //{
        //    //A.CallTo(() => _mediator.Send(A<CreateCustomerCommand>._, default)).Throws(new ArgumentException(exceptionMessage));

        //    var result = await _categoriesController.CreateCategory(_saveCategoryResource);

        //    (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        //    (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        //}

        //[Fact]
        //public async void Post_ShouldReturnCategory()
        //{
        //    var result = await _categoriesController.CreateCategory(_saveCategoryResource);

        //    (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        //    //result.Value.Should().BeOfType<Category>();
        //    //result.Value.Id.Should().Be(_id);
        //}

        //[Fact]
        //public async void Put_ShouldReturnCategory()
        //{
        //    var result = await _categoriesController.UpdateCategory(3, _updateCategoryResource);

        //    (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        //    //result.Value.Should().BeOfType<Category>();
        //    //result.Value.Id.Should().Be(_id);
        //}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async Task<IEnumerable<Feedback>> GetAllWithUser()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var feedbacks = new List<Feedback>
            {
                new Feedback { Id = 1, Name = "Feedback1", User = new User { Id = 1, Name = "User1"} },
                new Feedback { Id = 2, Name = "Feedback2", User = new User { Id = 2, Name = "User2"} },
                new Feedback { Id = 3, Name = "Feedback3", User = new User { Id = 3, Name = "User3"} },
                new Feedback { Id = 4, Name = "Feedback4", User = new User { Id = 4, Name = "User4"} },
                new Feedback { Id = 5, Name = "Feedback5", User = new User { Id = 5, Name = "User5"} }
            };
            return feedbacks;
        }
    }
}
