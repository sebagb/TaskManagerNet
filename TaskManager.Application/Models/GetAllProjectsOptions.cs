namespace TaskManager.Application.Models;

public class GetAllProjectsOptions
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? Title { get; set; }
}