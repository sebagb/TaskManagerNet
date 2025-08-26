using TaskManager.Application.Models;

namespace TaskManager.Application.Services;

public interface IProjectService
{
    public bool CreateProject(Project project);
}