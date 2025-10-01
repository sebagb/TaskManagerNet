using TaskManager.Application.Models;

namespace TaskManager.Application.Repositories;

public interface IMemberRepository
{
    public bool CreateMember(Member member);
    public Member? GetByCredentials(string username, string password);
    public bool Update(Member member);
}