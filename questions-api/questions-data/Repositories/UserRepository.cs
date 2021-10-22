using questions_data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace questions_data.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly IquestionsDbContext _context;
    public UserRepository(IquestionsDbContext dbContext) => _context = dbContext;

    public IEnumerable<User> GetUsers() => _context.Users.ToList();

    public User GetByLogin(string login) => _context.Users.FirstOrDefault(u => u.Login == login);
    public User GetByEmail(string email) => _context.Users.FirstOrDefault(u => u.Email == email);
    public User GetUser(int id) => _context.Users.Find(id);

    public void Create(User user)
    {
      _context.Users.Add(user);
      _context.SaveChanges();
    }
  }
}
