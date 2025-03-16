using Inventory.Management.Infrastructure.Data.EF.Model;

namespace Inventory.Management.Infrastructure.Interface
{
    public interface IMemberRepository
    {
        Task<Member?> GetMemberByIdAsync(int id);
        Task UpdateMemberAsync(Member member);
        Task AddMembersAsync(List<Member> members);

    }
}
