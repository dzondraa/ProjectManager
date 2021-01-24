using Application;
using Application.Commands;
using Application.DataTransfer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Project : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public Project(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<Project>
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(new { });
        }

        // GET api/<Project>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Project>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectDto dto, [FromServices] ICreateProjectCommandAsync command)
        {
            await _executor.ExecuteCommandAsync(command, dto);
            return Ok();
        }

        // PUT api/<Project>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Project>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
