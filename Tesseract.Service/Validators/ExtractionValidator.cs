using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tesseract.Domain.Entities;

namespace Tesseract.Service.Validators
{
    public class ExtractionValidator : AbstractValidator<ExtractionEntity>
    {
        public ExtractionValidator()
        {
            RuleFor(c => c.TextOcr)
                .NotEmpty().WithMessage("Please enter the TextOcr.")
                .NotNull().WithMessage("Please enter the TextOcr.");

            RuleFor(c => c.FileName)
                .NotEmpty().WithMessage("Please enter the FileName.")
                .NotNull().WithMessage("Please enter the FileName.");

            RuleFor(c => c.MeanConfidence)
                .NotEmpty().WithMessage("Please enter the MeanConfidence.")
                .NotNull().WithMessage("Please enter the MeanConfidence.");

            RuleFor(c => c.DateRegister)
                .NotEmpty().WithMessage("Please enter the DateRegister.")
                .NotNull().WithMessage("Please enter the DateRegister.");

            RuleFor(c => c.DateLastUpdate)
                .NotEmpty().WithMessage("Please enter the DateRegister.")
                .NotNull().WithMessage("Please enter the DateRegister.");
        }
    }
}
