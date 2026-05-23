using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Common.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Domain.Enums;

public class CreateTaskCommandHandler: IRequestHandler<CreateTaskCommand, TaskResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateTaskCommandHandler(IApplicationDbContext context,ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<TaskResponseDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId?? throw new UnauthorizedAccessException();

        var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == request.ProjectId && x.UserId == userId,cancellationToken);

        if (project is null)
            throw new KeyNotFoundException("Project not found");

        var task = new ProjectTask
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            Priority = request.Priority,
            Status = ProjectTaskStatus.Pending,
            ProjectId = request.ProjectId
        };

        await _context.ProjectTasks.AddAsync(task, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            Priority = task.Priority,
            Status = task.Status,
            ProjectId = task.ProjectId
        };
    }
}