using TaskManager.Application.Models;

namespace TaskManager.Application.Repositories;

public interface IProjectRepository
{
    public bool CreateProject(Project project);
    public bool CreateProjectTask(ProjectTask task);
    public bool DeleteById(Guid id);
    public bool DeleteProjectTaskById(Guid taskId);
    public IEnumerable<Project> GetAll();
    public Project? GetById(Guid id);
    public ProjectTask? GetProjectTaskById(Guid taskId);
}