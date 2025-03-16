using Inventory.Management.Infrastructure.Data.EF;
using Inventory.Management.Infrastructure.Data.EF.Model;
using Inventory.Management.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Management.Infrastructure.Implementation
{
    public class MemberRepository : IMemberRepository
    {
        private readonly InventoryDBContext _context;

        public MemberRepository(InventoryDBContext context)
        {
            _context = context;
        }

        public async Task AddMembersAsync(List<Member> members)
        {
            await _context.Members.AddRangeAsync(members);
            await _context.SaveChangesAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateMemberAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }
    }

}
