using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Common.Interfaces;

namespace ProjectManagement.Application.Features.Tasks.Commands.DeleteTask;

public class DeleteTaskCommandHandler: IRequestHandler<DeleteTaskCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public DeleteTaskCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId?? throw new UnauthorizedAccessException("User not authenticated");
        var task = await _context.ProjectTasks.Include(x => x.Project)
            .FirstOrDefaultAsync(x => x.Id == request.TaskId && x.Project.UserId == userId,cancellationToken);
        if (task is null)
        {
            throw new KeyNotFoundException("Task not found");
        }
        _context.ProjectTasks.Remove(task);
        await _context.SaveChangesAsync(cancellationToken);
    }
}