using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Common.Interfaces;
using ProjectManagement.Application.Features.Projects.DTOs;

namespace ProjectManagement.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectCommandHandler: IRequestHandler<UpdateProjectCommand,ProjectResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICacheService _cacheService;

    public UpdateProjectCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, ICacheService cacheService)
    {
        _context = context;
        _currentUserService =currentUserService;
        _cacheService = cacheService;
    }

    public async Task<ProjectResponseDto>
        Handle(UpdateProjectCommand request ,CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId?? throw new UnauthorizedAccessException("User not authenticated");

        var project = await _context.Projects.FirstOrDefaultAsync(x =>x.Id == request.Id &&x.UserId == userId,cancellationToken);

        if (project is null)
        {
            throw new KeyNotFoundException("Project not found");
        }

        project.Name = request.Name;
        project.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);
        await _cacheService.RemoveAsync($"projects_{userId}");

        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedAt = project.CreatedAt
        };
    }
}