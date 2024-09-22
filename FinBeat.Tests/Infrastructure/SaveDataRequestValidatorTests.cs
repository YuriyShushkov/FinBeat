using FinBeat.Application.DTOs;
using FinBeat.Application.Validators;
using FluentValidation.TestHelper;

namespace FinBeat.Tests.Infrastructure
{
    public class SaveDataRequestValidatorTests
    {
        private readonly DataRecordValidator _validator;

        public SaveDataRequestValidatorTests()
        {
            _validator = new DataRecordValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Key_Is_Negative()
        {
            var result = _validator.TestValidate(new DataRecordDto(-1, "validValue"));
            result.ShouldHaveValidationErrorFor(d => d.Code);
        }

        [Fact]
        public void Should_Have_Error_When_Value_Is_More_Than_255()
        {
            var result = _validator.TestValidate(new DataRecordDto(1, new string('1', 270)));
            result.ShouldHaveValidationErrorFor(d => d.Value);
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Data()
        {
            var result = _validator.TestValidate(new DataRecordDto(1, "validValue"));
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
