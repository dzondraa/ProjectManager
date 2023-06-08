using Application;
using Domain.Entities;
using EFDataAccess;
using System;
using static EFDataAccess.ProjectManagementContext;

namespace Implementation.Implementation
{
    public class DbUseCaseLogger : IUseCaseLogger
    {
        private readonly ProjectManagementContextFactory _contextFactory;

        public DbUseCaseLogger(ProjectManagementContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void Log(IUseCase useCase, object useCaseData, IApplicationActor actor)
        {
            var database = _contextFactory.CreateDbContext(null);
            database.AuditLogs.Add(new AuditLog
            {
                Action = useCase.Name,
                Actor = actor.Email,
                Timestamp = DateTime.UtcNow,
            });
            database.SaveChanges();
            Console.WriteLine($"[{DateTime.Now}] [Actor: {actor?.Email}] Running action {useCase.Name}");
        }
    }
}
