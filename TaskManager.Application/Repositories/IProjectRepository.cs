using TaskManager.Application.Models;

namespace TaskManager.Application.Repositories;

public interface IProjectRepository
{
    public bool CreateProject(Project project);
    public bool DeleteById(Guid id);
    public IEnumerable<Project> GetAll();
    public Project? GetById(Guid id);
}