using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFDataAccess
{
    public class ProjectManagementContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<WorkItem> WorkItems { get; set; }

        public DbSet<WorkItemType> WorkItemTypes { get; set; }

        public DbSet<Reaction> Reactions { get; set; }

        public DbSet<ReactionType> ReactionTypes { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<WorkItemAttachments> WorkItemAttachments { get; set; }


        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);
        }
    }
}
