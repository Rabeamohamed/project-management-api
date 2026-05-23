using FluentValidation;

namespace ProjectManagement.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator: AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}