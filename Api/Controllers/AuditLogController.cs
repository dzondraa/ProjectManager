using Application.Queries;
using Application.Searches;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static EFDataAccess.ProjectManagementContext;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditLogController : ControllerBase
    {
        private readonly ProjectManagementContextFactory _contextFactory;

        public AuditLogController(ProjectManagementContextFactory projectManagementContextFactory)
        {
            _contextFactory = projectManagementContextFactory;
        }

        // POST api/<AuthController>
        [HttpGet]
        public IActionResult Get([FromQuery] AuditLogSearch search)
        {
            var database = _contextFactory.CreateDbContext(null);
            var query = database.AuditLogs.AsQueryable();

            if(search.Timestamp.HasValue)
            {
                query = query.Where(x => x.Timestamp.Date == search.Timestamp.Value.Date);
            }
            if (!string.IsNullOrEmpty(search.Action))
            {
                query = query.Where(x => x.Action.ToLower() == search.Action.ToLower());
            }
            if (!string.IsNullOrEmpty(search.Actor))
            {
                query = query.Where(x => x.Actor.ToLower() == search.Actor.ToLower());
            }

            query = query.Skip((search.PageNumber - 1) * search.PageSize).Take(search.PageSize);

            return Ok(new PagedResponse<AuditLog>
            {
                Items = query.ToList(),
                TotalCount = query.Count(),
            });
        }
    }
}
