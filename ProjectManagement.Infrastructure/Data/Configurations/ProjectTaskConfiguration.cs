using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagement.Domain.Entities;

namespace ProjectManagement.Infrastructure.Data.Configurations
{
    public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Priority)
                .IsRequired();

            builder.HasOne(x => x.Project)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
