using Microsoft.EntityFrameworkCore;
using questions_data.Entities;

namespace questions_data
{
  public interface IquestionsDbContext
  {
    DbSet<User> Users { get; set; }

    int SaveChanges();
  }
}
