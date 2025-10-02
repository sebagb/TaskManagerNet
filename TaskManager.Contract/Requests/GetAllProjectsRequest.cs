namespace TaskManager.Contract.Requests;

public class GetAllProjectsRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}