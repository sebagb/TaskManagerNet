using TaskManager.Application.Models;

namespace TaskManager.Application.Repositories;

public interface IProjectRepository
{
    public bool CreateProject(Project project);
}