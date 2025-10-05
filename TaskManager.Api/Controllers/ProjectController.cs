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

        return NoContent();
    }

    [HttpGet(ApiEndpoints.Project.GetAll)]
    public IActionResult GetAll([FromQuery] GetAllProjectsRequest request)
    {
        var options = request.MapToOptions();
        var projects = service.GetAll(options);

        if (projects.IsNullOrEmpty())
        {
            return NotFound("The are not that many projects yet");
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

    [HttpPost(ApiEndpoints.Project.CreateTask)]
    [Authorize]
    public IActionResult CreateTask(
        [FromRoute] Guid projectId,
        [FromBody] CreateProjectTaskRequest request)
    {
        var task = request.MapToProjectTask(projectId);
        var result = service.CreateProjectTask(task);
        if (!result)
        {
            return NotFound("There are no projects with requested id");
        }

        var response = task.MapToResponse();
        return CreatedAtAction(nameof(Get), new { id = projectId }, response);
    }

    [HttpGet(ApiEndpoints.Project.GetTask)]
    public IActionResult GetTask(
        [FromRoute] Guid projectId,
        [FromRoute] Guid taskId)
    {
        var task = service.GetProjectTaskById(taskId);

        if (task == null)
        {
            return NotFound();
        }

        var response = task.MapToResponse();
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Project.DeleteTask)]
    [Authorize]
    public IActionResult DeleteTask(
        [FromRoute] Guid projectId,
        [FromRoute] Guid taskId)
    {
        var result = service.DeleteProjectTaskById(taskId);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}