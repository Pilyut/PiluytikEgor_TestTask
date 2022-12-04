using Infrastructure.Repositories.Interfaces;
using Infrastructure.DataBase;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonsDbContext _context;

        public PersonRepository(PersonsDbContext context)
        {
            _context = context;
        }

        public async Task<List<PersonModel>> GetByRange(int from, int to)
        {
            var take = await _context.Persons
                .Skip(from)
                .Take(to)
                .ToListAsync();
            return take;
        }
    }
}
