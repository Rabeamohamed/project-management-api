
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Infrastructure.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.HasMany(x => x.Tasks)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
