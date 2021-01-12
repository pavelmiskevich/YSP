using System;
using System.Text;
using FluentValidation.TestHelper;
using Xunit;
using YSP.Api.Validators;

namespace CustomerApi.Test.Validator.v1
{
    public class SaveCategoryResourceValidatorTests
    {
        private readonly SaveCategoryResourceValidator _testee;

        public SaveCategoryResourceValidatorTests()
        {
            _testee = new SaveCategoryResourceValidator();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Name_WhenNullOrEmpty_ShouldHaveValidationError(string name)
        {
            _testee.ShouldHaveValidationErrorFor(x => x.Name, name).WithErrorMessage("'Name' must not be empty and less or equal 250.");
        }

        [Fact]
        public void Name_WhenLonger_ShouldHaveValidationError()
        {
            StringBuilder sb = new StringBuilder(250);
            _testee.ShouldHaveValidationErrorFor(x => x.Name, sb.ToString()).WithErrorMessage("'Name' must not be empty and less or equal 250.");
        }
        

        [Fact]
        public void Name_WhenNotEmptyAndShorterMax_ShouldNotHaveValidationError()
        {
            _testee.ShouldNotHaveValidationErrorFor(x => x.Name, "Name_WhenNotEmptyAndShorterMax_ShouldNotHaveValidationError");
        }
    }
}