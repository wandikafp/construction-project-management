using Application.Dtos.Request;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ConstructionProjectController : ControllerBase
    {
        private readonly IConstructionProjectService _service;

        public ConstructionProjectController(IConstructionProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string query = "")
        {
            var projects = await _service.GetAllProjectsAsync(pageNumber, pageSize, query);
            var response = new
            {
                projects.PageIndex,
                projects.TotalPages,
                projects.TotalCount,
                projects.HasPreviousPage,
                projects.HasNextPage,
                Projects = projects
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _service.GetProjectByIdAsync(id);
            return project == null ? NotFound() : Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ConstructionProjectRequest project)
        {
            string message;
            if(!project.IsValid(out message))
            {
                return BadRequest(message);
            }
            var createdProject = await _service.CreateProjectAsync(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.ProjectId }, createdProject);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] ConstructionProjectRequest project)
        {
            string message;
            if (!project.IsValid(out message))
            {
                return BadRequest(message);
            }
            await _service.UpdateProjectAsync(project);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _service.DeleteProjectAsync(id);
            return NoContent();
        }
    }
   
}
