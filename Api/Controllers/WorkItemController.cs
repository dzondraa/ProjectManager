using Application;
using Application.Commands;
using Application.Queries;
using Application.Requests;
using Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkItemController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public WorkItemController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        [HttpGet]
        public IActionResult Get(
            [FromQuery] WorkItemSearch search,
            [FromServices] IGetWorkItemsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        [HttpGet("{id}")]
        public IActionResult Get(
            int id,
            [FromServices] IGetWorkItemQuery query)
        {
            var data = _executor.ExecuteQuery(query, id);

            if (data is null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Post(
            [FromBody] CreateWorkItemRequest request,
            [FromServices] ICreateWorkItem command)
        {
            await _executor.ExecuteCommandAsync(command, request);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> Put(
            int id,
            [FromBody] UpdateWorkItemRequest request,
            [FromServices] IUpdateWorkItemCommand updateRoleCommand)
        {
            request.Id = id;
            await _executor.ExecuteCommandAsync(updateRoleCommand, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> Delete(
           int id,
           [FromServices] IDeleteWorkItemCommand deleteCommand)
        {
            await _executor.ExecuteCommandAsync(deleteCommand, id);
            return NoContent();
        }
    }
}
