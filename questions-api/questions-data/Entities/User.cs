using System;
using System.Collections.Generic;
using System.Text;

namespace questions_data.Entities
{
  public class User
  {
    public int Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] Salt { get; set; }
  }
}
