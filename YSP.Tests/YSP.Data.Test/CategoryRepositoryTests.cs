using CustomerApi.Data.Test.Infrastructure;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using YSP.Core.Models;
using YSP.Data.Repositories;

namespace YSP.Data.Test
{
    public class CategoryRepositoryTests : DatabaseTestBase
    {
        private readonly YSPDbContext _context;
        private readonly CategoryRepository _testee;
        private readonly CategoryRepository _testeeFake;
        private readonly Category _newCategory;
        private readonly Mock<YSPDbContext> _mock;

        public CategoryRepositoryTests()
        {
            _mock = new Mock<YSPDbContext>(options);
            _context = _mock.Object;
            _testeeFake = new CategoryRepository(_context);
            _testee = new CategoryRepository(Context);
            _newCategory = new Category
            {
                Name = "NewCategory",
                ParentId = 1                
            };            
        }

        [Fact]
        public void GetAllAsync_WhenExceptionOccurs_ThrowsExceptiont()
        {
            _mock.Setup(repo => repo.Set<Category>()).Throws(new Exception());
            _testeeFake.Invoking(x => x.GetAllAsync()).Should().Throw<Exception>().WithMessage("Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public void GetAllAsync_WhenNoData_ShouldReturnEmptyList()
        {
            //var result = await _testeeFake.GetAllAsync();
            _testeeFake.Invoking(x => x.GetAllAsync()).Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'source')");
        }

        [Fact]
        public async void GetAllAsync_WhenExistData_ShouldReturnNotEmptyList()
        {
            var result = await _testee.GetAllAsync();

            result.Should().NotBeNull();
            result.Should().BeOfType<List<Category>>();
            result.Count().Should().NotBe(0);
        }

        [Theory]
        [InlineData("ChangedCategory")]
        public async void AddAsync_WhenCategoryIsNotNull_ShouldReturnCategory(string name)
        {
            var categoryCount = Context.Categories.Count();
            //var category = Context.Categories.First();
            _newCategory.Name = name;

            await _testee.AddAsync(_newCategory);
            await Context.SaveChangesAsync();
            var result = Context.Categories.Last();

            result.Should().NotBeNull();
            result.Should().BeOfType<Category>();
            result.Name.Should().Be(name);
            Context.Categories.Count().Should().Be(categoryCount + 1);

            // Мы не передаем методу Verify никаких дополнительных параметров.
            // Это значит, что будут использоваться ожидания установленные
            // с помощью mock.Setup
            //mock.Verify();
        }

        [Fact]
        public void AddAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.AddAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddAsync_WhenExceptionOccurs_ThrowsException()
        {
            _testeeFake.Invoking(x => x.AddAsync(new Category())).Should().Throw<Exception>().WithMessage("Object reference not set to an instance of an object.");
        }
    }
}
