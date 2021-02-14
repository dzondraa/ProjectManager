using Api.Searches;
using Application;
using Application.Commands;
using Application.DataTransfer;
using Application.Queries;
using Application.Requests;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public Project(UseCaseExecutor executor, IMapper mapper)
        {
            _mapper = mapper;
            _executor = executor;
        }

        // GET: api/<Project>
        [HttpGet]
        public IActionResult Get([FromQuery] ProjectSearch search, [FromServices] IQueryProject query)
        { 
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<Project>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromServices] IGetProject query)
        {
            var result = _executor.ExecuteQuery(query, new ProjectDto { Id = id });
            return Ok(result);
        }

        // POST api/<Project>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectRequest dto, [FromServices] ICreateProjectCommandAsync command)
        {
            await _executor.ExecuteCommandAsync(command, dto);
            return Ok();
        }

        // PUT api/<Project>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ProjectRequest request, [FromServices] IUpdateProjectCommandAsync command)
        {
            var reqestAsDto = _mapper.Map<ProjectDto>(request);
            reqestAsDto.Id = id;
            await _executor.ExecuteCommandAsync(command, reqestAsDto);
            return Ok();
        }

        // DELETE api/<Project>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromServices] IDeleteProjectAsync command)
        {
            
            await _executor.ExecuteCommandAsync(command, new ProjectDto { Id = id});
            return Ok();
        }
    }
}
