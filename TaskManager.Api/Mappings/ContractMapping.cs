using TaskManager.Application.Models;
using TaskManager.Contract.Requests;
using TaskManager.Contract.Responses;

namespace TaskManager.Api.Mappings;

public static class ContractMapping
{
    public static Project MapToProject(this CreateProjectRequest request)
    {
        var projectId = Guid.NewGuid();
        return new Project()
        {
            Id = projectId,
            Deadline = request.Deadline,
            Status = Project.State.Pending,
            Title = request.Title,
            Tasks = [new ProjectTask() {
                Id = Guid.NewGuid(),
                ProjectId = projectId,
                Title = "TODO" }]
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

    public static Member MapToMember(this CreateMemberRequest request)
    {
        return new Member()
        {
            MemberId = Guid.NewGuid(),
            Username = request.Username,
            Password = request.Password,
        };
    }

    public static MemberResponse MapToResponse(this Member member)
    {
        return new MemberResponse()
        {
            MemberId = member.MemberId,
            Username = member.Username,
            Password = member.Password,
            IsAdmin = member.IsAdmin
        };
    }

    public static Member MapToMember(this UpdateTrustedMemberRequest request, Guid memberId)
    {
        return new Member()
        {
            MemberId = memberId,
            Username = request.Username,
            Password = request.Password,
            IsAdmin = request.IsAdmin
        };
    }

    public static ProjectTask MapToProjectTask(this CreateProjectTaskRequest request, Guid projectId)
    {
        return new ProjectTask()
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId,
            Title = request.Title,
            Priority = request.Priority
        };
    }

    public static ProjectTaskResponse MapToResponse(this ProjectTask project)
    {
        return new ProjectTaskResponse()
        {
            Id = project.Id,
            ProjectId = project.ProjectId,
            Title = project.Title,
            Priority = project.Priority,
        };
    }
}