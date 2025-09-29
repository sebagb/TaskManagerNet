using TaskManager.Application.Models;
using TaskManager.Contract.Requests;
using TaskManager.Contract.Responses;

namespace TaskManager.Api.Mappings;

public static class ContractMapping
{
    public static Project MapToProject(this CreateProjectRequest request)
    {
        return new Project()
        {
            Id = Guid.NewGuid(),
            Deadline = request.Deadline,
            Status = Project.State.Pending,
            Title = request.Title
        };
    }

    public static ProjectResponse MapToResponse(this Project project)
    {
        return new ProjectResponse()
        {
            Id = project.Id,
            Deadline = project.Deadline,
            Status = project.Status.ToString(),
            Tasks = project.Tasks,
            Title = project.Title
        };
    }
}