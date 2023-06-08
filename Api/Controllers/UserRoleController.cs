using Application;
using Application.Commands;
using Application.Queries;
using Application.Requests;
using Application.Searches;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public UserRoleController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<UserRoleController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] RoleSearch search,
            [FromServices] IGetRolesQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<UserRoleController>/5
        [HttpGet("{id}")]
        public IActionResult Get(
            int id,
            [FromServices] IGetRoleQuery query)
        {
            var data = _executor.ExecuteQuery(query, id);
            
            if (data is null) 
                return NotFound();
            
            return Ok(data); 
        }

        // POST api/<UserRoleController>
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Post(
            [FromBody] CreateRoleRequest request,
            [FromServices] ICreateRole createRoleCommand)
        {
            await _executor.ExecuteCommandAsync(createRoleCommand, request);
            return NoContent();
        }

        // PATCH api/<UserRoleController>/5
        [HttpPatch("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> Put(
            int id, 
            [FromBody] UpdateRoleRequest request,
            [FromServices] IUpdateRoleCommand updateRoleCommand)
        {
            await _executor.ExecuteCommandAsync(updateRoleCommand, request);
            return NoContent();
        }

        // DELETE api/<UserRoleController>/5
        [HttpDelete("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> Delete(
            int id,
            [FromServices] IDeleteRoleCommand deleteRoleCommand)
        {
            await _executor.ExecuteCommandAsync(deleteRoleCommand, id);
            return NoContent();
        }
    }
}
