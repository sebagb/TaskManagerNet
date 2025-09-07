using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Mappings;
using TaskManager.Application.Services;
using TaskManager.Contract.Requests;

namespace TaskManager.Api.Controllers;

public class ProjectController
    (IProjectService service)
    : ControllerBase
{
    [HttpPost(ApiEndpoints.Project.CreateProject)]
    public IActionResult CreateProject(
        [FromBody] CreateProjectRequest request)
    {
        var project = request.MapToProject();
        service.CreateProject(project);
        var response = project.MapToResponse();
        return CreatedAtAction(nameof(CreateProject), new { id = project.Id }, response);
    }
}