using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Common.Interfaces;
using ProjectManagement.Application.Features.Projects.DTOs;

namespace ProjectManagement.Application.Features.Projects.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectResponseDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICacheService _cacheService;

        public GetAllProjectsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService , ICacheService cacheService)
        {
            _context = context;
            _currentUserService = currentUserService;
            _cacheService = cacheService;
        }

        public async Task<List<ProjectResponseDto>>Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException("User not authenticated");
            
            var cacheKey = $"projects_{userId}";
            var cachedProjects = await _cacheService.GetAsync<List<ProjectResponseDto>>(cacheKey);
            if (cachedProjects is not null)
            {
                Console.WriteLine("FROM CACHE");
                return cachedProjects;
            }

            Console.WriteLine("FROM DATABASE");
            var projects = await _context.Projects.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt)
                .Select(x => new ProjectResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt
                }).ToListAsync(cancellationToken);

            await _cacheService.SetAsync(cacheKey,projects,TimeSpan.FromMinutes(10));
            return projects;
        }
    }
}
