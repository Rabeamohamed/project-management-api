using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Common.Interfaces;

namespace ProjectManagement.Application.Features.Tasks.Queries.GetTasksByProject;

public class GetTasksByProjectQueryHandler : IRequestHandler<GetTasksByProjectQuery, List<TaskResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    public GetTasksByProjectQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }
    public async Task<List<TaskResponseDto>> Handle(GetTasksByProjectQuery request,CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId?? throw new UnauthorizedAccessException();

        var projectExists = await _context.Projects.AnyAsync(x => x.Id == request.ProjectId &&x.UserId == userId,cancellationToken);

        if (!projectExists)
        {
            throw new KeyNotFoundException("Project not found");
        }

        var query = _context.ProjectTasks.Where(x => x.ProjectId == request.ProjectId);

        // Search by title or description
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(x => x.Title.Contains(request.Search) || x.Description!.Contains(request.Search));
        }

        // Filter by status
        if (request.Status.HasValue)
        {
            query = query.Where(x =>(int)x.Status == request.Status);
        }

        // Filter by priority
        if (request.Priority.HasValue)
        {
            query = query.Where(x => (int)x.Priority == request.Priority);
        }

        query = query.OrderByDescending(x => x.CreatedAt);

        var tasks = await query.Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new TaskResponseDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                DueDate = x.DueDate,
                Priority = x.Priority,
                ProjectId = x.ProjectId
            })
            .ToListAsync(cancellationToken);

        return tasks;
    }
}