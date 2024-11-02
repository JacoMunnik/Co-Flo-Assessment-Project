using Microsoft.EntityFrameworkCore;

public class PeopleContext(DbContextOptions<PeopleContext> options) : DbContext(options)
{
    public DbSet<Person> People { get; set; }
}