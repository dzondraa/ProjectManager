using Application.DataTransfer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validations
{
    public class ProjectRequestValidation : AbstractValidator<ProjectDto>
    {
        public ProjectRequestValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
