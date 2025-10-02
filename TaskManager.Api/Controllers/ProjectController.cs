using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Api.Auth;
using TaskManager.Api.Mappings;
using TaskManager.Application.Services;
using TaskManager.Contract.Requests;

namespace TaskManager.Api.Controllers;

public class ProjectController
    (IProjectService service)
    : ControllerBase
{
    [HttpPost(ApiEndpoints.Project.Create)]
    [Authorize(AuthConstants.AdminUserPolicyName)]
    public IActionResult Create(
        [FromBody] CreateProjectRequest request)
    {
        var project = request.MapToProject();
        service.CreateProject(project);
        var response = project.MapToResponse();
        return CreatedAtAction(nameof(Get), new { id = project.Id }, response);
    }

    [HttpDelete(ApiEndpoints.Project.Delete)]
    [Authorize(AuthConstants.AdminUserPolicyName)]
    public IActionResult Delete(
        [FromRoute] Guid id)
    {
        var result = service.DeleteById(id);

        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpGet(ApiEndpoints.Project.GetAll)]
    public IActionResult GetAll()
    {
        var projects = service.GetAll();

        if (projects.IsNullOrEmpty())
        {
            return NotFound("The are no projects yet");
        }

        var response = projects.Select(x => x.MapToResponse());
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Project.GetById)]
    public IActionResult Get(
        [FromRoute] Guid id)
    {
        var project = service.GetById(id);

        if (project == null)
        {
            return NotFound();
        }

        var response = project.MapToResponse();
        return Ok(response);
    }

    //Trusted: add task
}