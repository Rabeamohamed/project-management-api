using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Project> Projects { get; }
        DbSet<ProjectTask> ProjectTasks { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
