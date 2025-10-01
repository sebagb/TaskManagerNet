using TaskManager.Application.Models;
using TaskManager.Application.Repositories;

namespace TaskManager.Application.Services;

public class MemberService
    (IMemberRepository repository)
    : IMemberService
{
    private readonly IMemberRepository repository = repository;

    public bool CreateMember(Member member)
    {
        return repository.CreateMember(member);
    }

    public Member? GetByCredentials(string username, string password)
    {
        return repository.GetByCredentials(username, password);
    }

    public bool Update(Member member)
    {
        return repository.Update(member);
    }
}
