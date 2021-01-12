using System;
using System.IO;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using YSP.Data;
using YSP.Core;
using YSP.Core.Models;
using YSP.Core.Services;

namespace YSP.Services.Test
{
    //https://docs.microsoft.com/ru-ru/dotnet/core/testing/unit-testing-with-dotnet-test

    public class CategoryServiceTest
    {
        private readonly ICategoryService _categoryService;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryServiceTest()
        {
            #region appsettings.json            
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            #endregion appsettings.json
            #region InitialDB
            var optionsBuilder = new DbContextOptionsBuilder<YSPDbContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            var context = new YSPDbContext(options);
            //using (var context = new YSPDbContext(options))
            //{
            //    //await Datalnitializer.RecreateDatabaseAsync(context);
            //    await Datalnitializer.ClearDataAsync(context);
            //    await Datalnitializer.InitializeDataAsync(context);

            //    foreach (var user in context.Users)
            //    {
            //        Console.WriteLine($"{user.Name} {user.Password}");
            //    }
            //}
            #endregion InitialDB 

            this._unitOfWork = new UnitOfWork(context);
            this._categoryService = new CategoryService(_unitOfWork);
        }

        [Fact]
        public async void CategoryService_GetCategoryById_ReturnCategory()
        {
            var actual = await _categoryService.GetCategoryById(1);
            var expected = 1;

            Assert.Equal(expected, actual.Id);
            //Assert.False(result.Id, "1 should not be prime");
        }

        [Fact]
        public async void CategoryService_GetCategoryById_ReturnNull()
        {
            var actual = await _categoryService.GetCategoryById(-1);
            var expected = default(Category);

            Assert.Equal(expected, actual);
        }
    }
}
