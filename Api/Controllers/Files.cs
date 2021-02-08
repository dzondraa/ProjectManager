using Application;
using Application.DataTransfer;
using Application.Queries;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Files : ControllerBase
    {

        private readonly UseCaseExecutor _executor;

        public Files(UseCaseExecutor executor)
        {
            _executor = executor;
        }
        // GET: api/<File>
        [HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<File>/5
        [HttpGet("{fileName}")]
        public async Task<IActionResult> Get(string fileName, [FromServices] IGetCode query)
        {

            var file = await _executor.ExecuteQueryAsync(query, new FileDto { Name = fileName });
            return File(file.Content, file.ContentType);

        }

        // POST api/<File>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<File>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<File>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
