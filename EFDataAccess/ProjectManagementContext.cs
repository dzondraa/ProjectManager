using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFDataAccess
{
    public class ProjectManagementContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Role> UserRoles { get; set; }
        
        public DbSet<Project> Projects { get; set; }

        public DbSet<WorkItem> WorkItems { get; set; }

        public DbSet<WorkItem> WorkItemTypes { get; set; }

        public DbSet<Reaction> Reactions { get; set; }
        
        public DbSet<ReactionType> ReactionTypes { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=ProjectManagement;Integrated Security=SSPI");
            base.OnConfiguring(optionsBuilder);
        }


        public class ProjectManagementContextFactory : IDesignTimeDbContextFactory<ProjectManagementContext>
        {
            public ProjectManagementContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ProjectManagementContext>();
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=ProjectManagement;Integrated Security=SSPI");

                return new ProjectManagementContext(optionsBuilder.Options);
            }
        }
    }
}
