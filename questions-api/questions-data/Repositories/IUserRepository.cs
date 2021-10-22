using questions_data.Entities;
using System.Collections.Generic;

namespace questions_data.Repositories
{
  public interface IUserRepository
  {
    User GetByLogin(string login);
    User GetByEmail(string email);
    IEnumerable<User> GetUsers();
    void Create(User user);
    User GetUser(int id);
  }
}
