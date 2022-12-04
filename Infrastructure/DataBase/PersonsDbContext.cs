using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.DataBase
{
    public class PersonsDbContext : DbContext
    {
        public DbSet<PersonModel> Persons { get; set; } = null!;

        public PersonsDbContext(DbContextOptions<PersonsDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
