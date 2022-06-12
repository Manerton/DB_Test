using DB_Test.EntityContext;
using DB_Test.Services;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<Users> Users { get; set; }
    public DbSet<Institutes> Institutes { get; set; }
    public DbSet<Specialties> Specialties { get; set; }
    public DbSet<Disciplines> Disciplines { get; set; }
    public DbSet<Groups> Groups { get; set; }
    public DbSet<Students> Students { get; set; }
    public DbSet<Tests> Tests { get; set; }
    public DbSet<Questions> Questions { get; set; }
    public DbSet<Answers> Answers { get; set; }
    public DbSet<GroupDisciplinesLink> GroupDisciplinesLinks { get; set; }
    public DbSet<TestsQuestionsLink> TestsQuestionsLink { get; set; }
    public DbSet<ResultTests> ResultTests { get; set; }
    
    public IAuthService _authService;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ResultTests;Username=postgres;Password=root");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Users>()
             .HasIndex(u => new { u.Email })
             .IsUnique();
        //modelBuilder.Entity<Questions>().HasCheckConstraint("Score", "Score > 0", c => c.HasName("CK_Question_Score"));
        //modelBuilder.Entity<ResultTests>().HasCheckConstraint("SumScore", "SumScore > 0", c => c.HasName("CK_ResultTests_SumScore"));
        modelBuilder.Entity<Institutes>()
            .HasIndex(x => x.InstitutesName).IsUnique();

        modelBuilder.Entity<Specialties>()
           .HasIndex(x => x.SpecialtiesName).IsUnique();

        modelBuilder.Entity<Disciplines>()
           .HasIndex(x => x.DisciplineName).IsUnique();

        modelBuilder.Entity<Groups>()
           .HasIndex(x => x.GroupName).IsUnique();

        modelBuilder.Entity<Tests>()
         .HasIndex(x => x.Name).IsUnique();

        modelBuilder.Entity<GroupDisciplinesLink>()
            .HasIndex(p => new { p.GroupId, p.DisciplineId })
            .IsUnique(true);

        modelBuilder.Entity<TestsQuestionsLink>()
            .HasIndex(p => new { p.TestId, p.QuestionId })
            .IsUnique(true);

   
    }
}
