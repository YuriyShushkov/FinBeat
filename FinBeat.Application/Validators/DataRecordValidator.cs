using FinBeat.Application.DTOs;
using FluentValidation;

namespace FinBeat.Application.Validators
{
    public class DataRecordValidator : AbstractValidator<DataRecordDto>
    {
        public DataRecordValidator()
        {
            RuleFor(record => record.Code)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Code must be a non-negative integer.");

            RuleFor(record => record.Value)
                .NotEmpty()
                .WithMessage("Value is required.")
                .MaximumLength(255)
                .WithMessage("Value cannot be longer than 255 characters.");
        }
    }
}
