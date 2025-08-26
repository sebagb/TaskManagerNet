namespace TaskManager.Application.Models;

public class Project
{
    public int Id { get; set; }
    public int Deadline { get; set; }
    public string Status { get; set; }
    public string Tasks { get; set; }
    public string Title { get; set; }
}