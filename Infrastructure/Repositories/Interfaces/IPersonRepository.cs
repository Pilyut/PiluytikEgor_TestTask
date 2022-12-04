
using Domain.Models;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        public Task<List<PersonModel>> GetByRange(int from, int to);
    }
}
