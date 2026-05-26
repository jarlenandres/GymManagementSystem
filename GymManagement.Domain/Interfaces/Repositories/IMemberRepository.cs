using GymManagement.Domain.Entities;

namespace GymManagement.Domain.Interfaces.Repositories;

public interface IMemberRepository : IGenericRepository<Member>
{
    Task<Member?> GetByNameAsync(string name);

    Task<Member?> GetByDocument(int document);
}
