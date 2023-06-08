using EFDataAccess;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Implementation
{
    public class ProjectManagementContextFactory : IDesignTimeDbContextFactory<ProjectManagementContext>
    {
        private readonly AppSettings _appSettings;

        public ProjectManagementContextFactory(AppSettings appSettings)
        { 
            _appSettings = appSettings;
        }

        public ProjectManagementContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectManagementContext>();
            optionsBuilder.UseSqlServer(_appSettings.connectionStrings.DB);

            return new ProjectManagementContext(optionsBuilder.Options);
        }
    }
}
