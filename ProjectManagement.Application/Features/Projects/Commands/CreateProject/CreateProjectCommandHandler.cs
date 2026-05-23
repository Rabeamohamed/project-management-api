using MediatR;
using ProjectManagement.Application.Common.Interfaces;
using ProjectManagement.Application.Features.Projects.DTOs;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectResponseDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICacheService _cacheService;

    public CreateProjectCommandHandler(IApplicationDbContext context,ICurrentUserService currentUserService, ICacheService cacheService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _cacheService = cacheService;
    }

    public async Task<ProjectResponseDto> Handle(CreateProjectCommand request,CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId?? throw new UnauthorizedAccessException("User not authenticated");

        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Projects.AddAsync(project, cancellationToken);
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