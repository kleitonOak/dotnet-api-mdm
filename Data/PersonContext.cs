using Microsoft.EntityFrameworkCore;
using Models;

public class PersonContext: DbContext
{
    public PersonContext(DbContextOptions<PersonContext> options): base(options)
    {

    }

    public DbSet<Person> people {get; set;}
}