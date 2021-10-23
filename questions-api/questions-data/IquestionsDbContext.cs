using Microsoft.EntityFrameworkCore;
using questions_data.Entities;
using System.Threading.Tasks;

namespace questions_data
{
  public interface IquestionsDbContext
  {
    DbSet<User> Users { get; set; }
    DbSet<Question> Questions { get; set; }
    DbSet<Answer> Answers { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync();
  }
}
