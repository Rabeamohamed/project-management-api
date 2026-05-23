using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Common.Interfaces;

namespace ProjectManagement.Application.Features.Projects.Commands.DeleteProject;

public class DeleteProjectCommandHandler: IRequestHandler<DeleteProjectCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly ICacheService _cacheService;

    public DeleteProjectCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, ICacheService cacheService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _cacheService = cacheService;
    }
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId?? throw new UnauthorizedAccessException("User not authenticated");

        var project = await _context.Projects.FirstOrDefaultAsync( x => x.Id == request.Id && x.UserId == userId,cancellationToken);

        if (project is null)
        {
            throw new KeyNotFoundException("Project not found");
        }

        _context.Projects.Remove(project);

        await _context.SaveChangesAsync(cancellationToken);
        await _cacheService.RemoveAsync($"projects_{userId}");
    }
}