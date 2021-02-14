using Application.DataTransfer;
using Application.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Implementation.Validatiors
{
    public class ProjectRequestValidator : AbstractValidator<ProjectRequest>
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
