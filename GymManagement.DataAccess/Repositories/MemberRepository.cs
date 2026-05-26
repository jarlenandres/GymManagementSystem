using GymManagement.DataAccess.Context;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.DataAccess.Repositories;

public class MemberRepository : GenericRepository<Member>, IMemberRepository
{
    public MemberRepository(GymDbContext context) : base(context)
    {
    }

    public async Task<Member?> GetByNameAsync(string name)
    {
        return await _dbSet
            .FirstOrDefaultAsync(m => m.FirstName.ToLower() == name.ToLower());
    }

    public async Task<Member?> GetByDocument(int document)
    {
        return await _dbSet
            .FirstOrDefaultAsync(m => m.Document == document);
    }
}
