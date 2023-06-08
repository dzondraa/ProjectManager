using Application.Requests;
using FluentValidation;

namespace Implementation.Validatiors
{
    public class CreateWorkItemValidator : AbstractValidator<CreateWorkItemRequest>
    {
        public CreateWorkItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50);
        }
    }

    public class UpdateWorkItemValidator : AbstractValidator<UpdateWorkItemRequest>
    {
        public UpdateWorkItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50);
        }
    }
}
