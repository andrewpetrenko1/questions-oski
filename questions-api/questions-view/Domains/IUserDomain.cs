using questions_data.Entities;
using questions_view.ViewModels;
using System.Collections.Generic;

namespace questions_view.Domains
{
  public interface IUserDomain
  {
    int Register(UserView userView);
    int? Authenticate(UserLoginView userView);
    IEnumerable<User> GetUsers();
    User GetUser(int id);
    User GetUserEmail(string mail);
    string GenerateJWT(int userId);
    bool HasUser(string login);
  }
}
