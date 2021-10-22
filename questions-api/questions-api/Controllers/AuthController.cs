using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using questions_data.Entities;
using questions_view.Domains;
using questions_view.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace questions_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IUserDomain _domain;

    public AuthController(IUserDomain domain)
    {
      _domain = domain;
    }

    [HttpGet]
    public IActionResult Get()
    {
      return Ok(_domain.GetUsers());
    }

    [HttpGet("{id}")]
    [Authorize]
    public IActionResult Get(int id)
    {
      return Ok(_domain.GetUser(id));
    }

    private IActionResult GenToken(int id)
    {
      return Ok(new
      {
        access_token = _domain.GenerateJWT(id)
      });
    }

    [HttpPost("sign-in")]
    public IActionResult SignIn(UserLoginView userView)
    {
      var id = _domain.Authenticate(userView);
      if (id == null)
      {
        return UnprocessableEntity();
      }

      return GenToken((int)id);
    }

    [HttpPost("sign-up")]
    public IActionResult SignUp(UserView userView)
    {
      if (_domain.HasUser(userView.Login))
        return Conflict("This login already exists");
      if (_domain.GetUserEmail(userView.Email) != null)
        return Conflict("This email already exists");

      var id = _domain.Register(userView);

      return GenToken(id);
    }
  }
}
