using TaskManager.Application.Services;

namespace TaskManager.Console;

public class Client(IProjectService service)
{
    public readonly IProjectService Service = service;
}