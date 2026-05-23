using FluentValidation;

namespace ProjectManagement.Application.Features.Tasks.Commands.CreateTask;

public class CreateTaskCommandValidator
    : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Due date must be in the future");
    }
}