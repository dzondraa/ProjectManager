using Application;
using Application.DataTransfer;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static EFDataAccess.ProjectManagementContext;
using Microsoft.AspNetCore.Authorization;
using Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilesController : ControllerBase
    {

        private readonly UseCaseExecutor _executor;

        public FilesController(UseCaseExecutor executor)
        {
            _executor = executor;
        }
    
        [HttpGet("{projectName}")]
        public async Task<IActionResult> Get(string projectName, [FromServices] IGetCode query)
        {

            var file = await _executor.ExecuteQueryAsync(query, new FileDto { ProjectName = projectName });
            return File(file.Content, file.ContentType);

        }

        // POST api/<File>
        [HttpPost("{workItemId}")]
        public async Task<IActionResult> Post(
            IFormFile file, int workItemId, 
            [FromServices] ProjectManagementContextFactory contextFactory,
            [FromServices] AppSettings appSettings)
        {
            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            // Get the file name and extension
            var fileName = Path.GetFileName(file.FileName);
            var fileExtension = Path.GetExtension(fileName);

            // Generate a unique file name (optional)
            var uniqueFileName = Guid.NewGuid().ToString("N") + fileExtension;

            // Specify the path to save the file
            var filePath = Path.Combine(appSettings.FileServerRoot, uniqueFileName);

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var database = contextFactory.CreateDbContext(null);
            var newFileRecord = new Domain.Entities.File
            {
                Name = fileName,
                Type = fileExtension,
                Location = filePath,
                Size = (int)(file.Length * 0.000001),
            };
            var inserted = database.Files.Add(newFileRecord);
            database.SaveChanges();

            database.WorkItemAttachments.Add(new Domain.Entities.WorkItemAttachments
            {
                FileId = newFileRecord.Id,
                WorkItemId = workItemId
            });
            database.SaveChanges();
            // Return a success response or any other necessary information
            return Ok(new
            {
                FilePath = filePath,
            });
        }
    }
}
