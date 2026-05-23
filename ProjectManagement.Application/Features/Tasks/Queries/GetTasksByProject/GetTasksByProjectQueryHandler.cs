using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Common.Interfaces;

namespace ProjectManagement.Application.Features.Tasks.Queries.GetTasksByProject;

public class GetTasksByProjectQueryHandler: IRequestHandler<GetTasksByProjectQuery,List<TaskResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public GetTasksByProjectQueryHandler(IApplicationDbContext context,ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    public async Task<List<TaskResponseDto>> Handle(GetTasksByProjectQuery request,CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId?? throw new UnauthorizedAccessException();
        var projectExists = await _context.Projects.AnyAsync(x => x.Id == request.ProjectId && x.UserId == userId, cancellationToken);
        if (!projectExists)
        {
            throw new KeyNotFoundException( "Project not found");
        }
        return await _context.ProjectTasks.Where(x => x.ProjectId == request.ProjectId)
            .Select(x => new TaskResponseDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Status = x.Status,
            DueDate = x.DueDate,
            Priority = x.Priority,
            ProjectId = x.ProjectId
        }).ToListAsync(cancellationToken);
    }
}