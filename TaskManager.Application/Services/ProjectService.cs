using TaskManager.Application.Models;
using TaskManager.Application.Repositories;

namespace TaskManager.Application.Services;

public class ProjectService
    (IProjectRepository repository) : IProjectService
{
    private readonly IProjectRepository repository = repository;

    public bool CreateProject(Project project)
    {
        return repository.CreateProject(project);
    }

    public bool CreateProjectTask(ProjectTask task)
    {
        var projectId = task.ProjectId;
        var project = GetById(projectId);
        if (project == null)
        {
            return false;
        }
        return repository.CreateProjectTask(task);
    }

    public bool DeleteById(Guid id)
    {
        return repository.DeleteById(id);
    }

    public bool DeleteProjectTaskById(Guid taskId)
    {
        return repository.DeleteProjectTaskById(taskId);
    }

    public IEnumerable<Project> GetAll(GetAllProjectsOptions options)
    {
        return repository.GetAll(options);
    }

    public Project? GetById(Guid id)
    {
        return repository.GetById(id);
    }

    public ProjectTask? GetProjectTaskById(Guid taskId)
    {
        return repository.GetProjectTaskById(taskId);
    }
}
