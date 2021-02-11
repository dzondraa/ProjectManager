using Application.DataTransfer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validatiors
{
    public class ProjectRequestValidator : AbstractValidator<ProjectDto>
    {
        public ProjectRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(25);
        }
    }
}
