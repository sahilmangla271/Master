using Inventory.Management.Infrastructure.Data;
using Inventory.Management.Infrastructure.Data.EF.Model;
using Inventory.Management.Infrastructure.Interface;

namespace Inventory.Management.Infrastructure.Implementation
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IRepository<Data.EF.Model.Member> _repository;

        public MemberRepository(IRepository<Data.EF.Model.Member> repository)
        {
            _repository = repository;
        }

        public async Task AddMembersAsync(List<Member> members)
        {
            await _repository.AddRangeAsync(members);
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateMemberAsync(Member member)
        {
            await _repository.UpdateAsync(member);
        }
    }

}
