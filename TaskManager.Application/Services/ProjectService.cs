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

    public Project? GetById(int id)
    {
        return repository.GetById(id);
    }
}
