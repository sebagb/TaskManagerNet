using TaskManager.Application.Models;

namespace TaskManager.Contract.Responses;

public class ProjectResponse
{
    public Guid Id { get; set; }
    public required int Deadline { get; set; }
    public string? Status { get; set; }
    public List<ProjectTask>? Tasks { get; set; }
    public required string Title { get; set; }
}