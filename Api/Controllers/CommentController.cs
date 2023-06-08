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
    public class CommentController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public CommentController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        [HttpGet]
        public IActionResult Get(
            [FromQuery] CommentSearch search,
            [FromServices] IGetCommentsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        [HttpGet("{id}")]
        public IActionResult Get(
            int id,
            [FromServices] IGetCommentQuery query)
        {
            var data = _executor.ExecuteQuery(query, id);

            if (data is null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Post(
            [FromBody] CreateCommentRequest request,
            [FromServices] ICreateComment command)
        {
            await _executor.ExecuteCommandAsync(command, request);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async System.Threading.Tasks.Task<IActionResult> Put(
           int id,
           [FromBody] UpdateCommentRequest request,
           [FromServices] IUpdateComment updateRequest)
        {
            await _executor.ExecuteCommandAsync(updateRequest, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async System.Threading.Tasks.Task<IActionResult> Delete(
           int id,
           [FromServices] IDeleteComment deleteRoleCommand)
        {
            await _executor.ExecuteCommandAsync(deleteRoleCommand, id);
            return NoContent();
        }
    }
}
