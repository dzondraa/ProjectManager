using Api.Searches;
using Application;
using Application.Commands;
using Application.DataTransfer;
using Application.Queries;
using Application.Searches;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty Tasks, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Task  : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public Task(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<Task>
        [HttpGet]
        public IActionResult Get([FromQuery] TaskSearch search, [FromServices] IQueryTask query)
        { 
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<Task>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromServices] IGetTask query)
        {
            var result = _executor.ExecuteQuery(query, new TaskDto { Id = id.Split('$')[1], ProjectId = id.Split('$')[0] });
            return Ok(result);
        }

        //// POST api/<Task>
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] TaskDto dto, [FromServices] ICreateTaskCommandAsync command)
        //{
        //    await _executor.ExecuteCommandAsync(command, dto);
        //    return Ok();
        //}

        //// PUT api/<Task>/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(string id, [FromBody] TaskDto dto, [FromServices] IUpdateTaskCommandAsync command)
        //{
        //    dto.Id = id;
        //    await _executor.ExecuteCommandAsync(command, dto);
        //    return Ok();
        //}

        //// DELETE api/<Task>/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id, [FromServices] IDeleteTaskAsync command)
        //{

        //    await _executor.ExecuteCommandAsync(command, new TaskDto { Id = id});
        //    return Ok();
        //}
    }
}
