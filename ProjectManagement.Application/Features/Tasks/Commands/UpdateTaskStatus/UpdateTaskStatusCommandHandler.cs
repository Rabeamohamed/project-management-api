using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Common.Interfaces;

namespace ProjectManagement.Application.Features.Tasks.Commands.UpdateTaskStatus;

public class UpdateTaskStatusCommandHandler : IRequestHandler<UpdateTaskStatusCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateTaskStatusCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task Handle(UpdateTaskStatusCommand request,CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId?? throw new UnauthorizedAccessException("User not authenticated");

        var task = await _context.ProjectTasks.Include(x => x.Project)
            .FirstOrDefaultAsync(x =>x.Id == request.TaskId &&x.Project.UserId == userId,cancellationToken);

        if (task is null)
        {
            throw new KeyNotFoundException("Task not found");
        }

        task.Status = request.Status;

        await _context.SaveChangesAsync(cancellationToken);
    }
}