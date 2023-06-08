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
    public class UserController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public UserController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get(
            [FromQuery] UserSearch userSearch,
            [FromServices] IGetUsersQuery usersQuery)
        {
            return Ok(_executor.ExecuteQuery(usersQuery, userSearch));
        }

        //GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(
            int id,
            [FromServices] IGetSingleUsersQuery usersQuery)
        {
            return Ok(_executor.ExecuteQuery(usersQuery, id));
        }

        // POST api/<UserController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<IActionResult> Post(
            [FromBody] CreateUserRequest createUserRequest,
            [FromServices] ICreateUser createUserCommand)
        {
            await _executor.ExecuteCommandAsync(createUserCommand, createUserRequest);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<IActionResult> Put(
            int id, 
            [FromBody] UpdateUserRequest request,
            [FromServices] IUpdateUserCommand command)
        {
            request.UserId = id;
            await _executor.ExecuteCommandAsync(command, request);
            return NoContent();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<IActionResult> Delete(
            int id,
            [FromServices] IDeleteUserCommand command)
        {
            await _executor.ExecuteCommandAsync(command, id);
            return NoContent();
        }
    }
}
