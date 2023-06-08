using Application.Requests;
using FluentValidation;

namespace Implementation.Validatiors
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentRequest>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(200);
        }
    }

    public class UpdateCommentValidation : AbstractValidator<UpdateCommentRequest>
    {
        public UpdateCommentValidation()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MaximumLength(200);
            
        }
    }
}
