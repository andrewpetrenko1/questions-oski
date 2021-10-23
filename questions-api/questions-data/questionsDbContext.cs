using Microsoft.EntityFrameworkCore;
using questions_data.Entities;
using System.Threading.Tasks;

namespace questions_data
{
  public class questionsDbContext : DbContext, IquestionsDbContext
  {
    public questionsDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      var userModel = modelBuilder.Entity<User>();

      userModel
        .Property(u => u.Login)
        .IsRequired();

      userModel
        .Property(u => u.Email)
        .IsRequired();

      userModel
        .Property(u => u.PasswordHash)
        .IsRequired();

      userModel
        .Property(u => u.Salt)
        .IsRequired();

      var incQuestModel = modelBuilder.Entity<Answer>();

      incQuestModel
        .Property(q => q.TextAnswer)
        .IsRequired();

      incQuestModel
        .HasOne(iq => iq.Question)
        .WithMany(q => q.Answers)
        .HasForeignKey(q => q.QuestionId);

      var questionModel = modelBuilder.Entity<Question>();

      questionModel
        .Property(q => q.QuestionText)
        .IsRequired();

      questionModel
        .Property(q => q.CorrectAnswerId)
        .IsRequired();

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
  }
}
