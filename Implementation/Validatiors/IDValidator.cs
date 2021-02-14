using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validatiors
{
    public class IDValidator : AbstractValidator<string>
    {
        public IDValidator()
        {
            RuleFor(p => p)
                .Must(id => id.Contains("$"))
                .WithMessage(id => $"{id} is not a valid ID");
        }
    }
}
