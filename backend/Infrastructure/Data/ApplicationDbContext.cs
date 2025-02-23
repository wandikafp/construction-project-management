using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<ConstructionProject> ConstructionProjects { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConstructionProject>()
                .ToTable("construction_projects");
            modelBuilder.Entity<User>()
                .ToTable("users");
            modelBuilder.Entity<ConstructionProject>(entity =>
            {
                entity.Property(e => e.ProjectId).HasColumnName("project_id");
                entity.Property(e => e.ProjectName).HasColumnName("project_name");
                entity.Property(e => e.ProjectLocation).HasColumnName("project_location");
                entity.Property(e => e.Stage).HasColumnName("stage");
                entity.Property(e => e.Category).HasColumnName("category");
                entity.Property(e => e.OtherCategory).HasColumnName("other_category");
                entity.Property(e => e.StartDate).HasColumnName("start_date");
                entity.Property(e => e.ProjectDetails).HasColumnName("project_details");
                entity.Property(e => e.CreatorId).HasColumnName("creator_id");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });
        }

    }
}
