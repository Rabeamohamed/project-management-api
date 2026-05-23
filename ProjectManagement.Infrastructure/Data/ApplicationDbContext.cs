using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Application.Common.Interfaces;
using ProjectManagement.Domain.Entities;
using ProjectManagement.Infrastructure.Identity;

namespace ProjectManagement.Infrastructure.Data
{
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>,IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectTask> ProjectTasks => Set<ProjectTask>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}