using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Common.Interfaces;
using ProjectManagement.Application.Features.Projects.DTOs;

namespace ProjectManagement.Application.Features.Projects.Queries.GetProjectById;

public class GetProjectByIdQueryHandler: IRequestHandler<GetProjectByIdQuery,ProjectResponseDto>
{
    private readonly IApplicationDbContext _context;

    private readonly ICurrentUserService _currentUserService;

    public GetProjectByIdQueryHandler(IApplicationDbContext context,ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ProjectResponseDto>
        Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId?? throw new UnauthorizedAccessException("User not authenticated");

        var project = await _context.Projects
            .Where(x => x.Id == request.Id && x.UserId == userId)
            .Select(x => new ProjectResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreatedAt = x.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (project is null)
        {
            throw new KeyNotFoundException("Project not found");
        }

        return project;
    }
}