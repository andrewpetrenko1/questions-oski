using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace questions_view.ViewModels
{
  public class UserView
  {
    [Required]
    public string Login { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
  }
}
