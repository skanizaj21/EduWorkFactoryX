using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkTime> WorkTimes { get; set; }
        public DbSet<ProjectTime> ProjectTimes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<WorkTime>()
                .HasOne(w => w.User)
                .WithMany(u => u.WorkTimes)
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<ProjectTime>()
                .HasKey(pt => new { pt.UserId, pt.ProjectId });

            modelBuilder.Entity<ProjectTime>()
                .HasOne(pt => pt.User)
                .WithMany(u => u.ProjectTimes)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<ProjectTime>()
                .HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectTimes)
                .HasForeignKey(pt => pt.ProjectId);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.ProjectCategory)
                .WithMany(pc => pc.Projects)
                .HasForeignKey(p => p.ProjectCategoryId);

            modelBuilder.Entity<ProjectMember>()
                .HasKey(pm => new { pm.UserId, pm.ProjectId, pm.ProjectRoleId });

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany(u => u.ProjectMembers)
                .HasForeignKey(pm => pm.UserId);

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.Project)
                .WithMany(p => p.ProjectMembers)
                .HasForeignKey(pm => pm.ProjectId);

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.ProjectRole)
                .WithMany(pr => pr.ProjectMembers)
                .HasForeignKey(pm => pm.ProjectRoleId);
        }
    }
}
