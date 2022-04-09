using DB_Test.EntityContext;
using Microsoft.EntityFrameworkCore;

public class DB_Test_Context : DbContext
{
    public DbSet<Institutes> Institutes { get; set; }
    public DbSet<Specialties> Specialties { get; set; }
    public DbSet<Disciplines> Disciplines { get; set; }
    public DbSet<Groups> Groups { get; set; }
    public DbSet<Students> Students { get; set; }
    public DbSet<Tests> Tests { get; set; }
    public DbSet<Questions> Questions { get; set; }
    public DbSet<Answers> Answers { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ResultTests;Username=postgres;Password=root");
    }
}
