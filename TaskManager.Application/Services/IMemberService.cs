using TaskManager.Application.Models;

namespace TaskManager.Application.Services;

public interface IMemberService
{
    public bool CreateMember(Member member);
    public Member? GetByCredentials(string username, string password);
    public bool Update(Member member);
}