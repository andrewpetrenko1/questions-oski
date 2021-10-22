using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using questions_data.Entities;
using questions_data.Repositories;
using questions_view.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace questions_view.Domains
{
  public class UserDomain : IUserDomain
  {
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly SHA1 _sha1 = SHA1.Create();
    private readonly RNGCryptoServiceProvider _secureRandom = new RNGCryptoServiceProvider();

    public UserDomain(IUserRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public IEnumerable<User> GetUsers() => _repository.GetUsers();

    public User GetUser(int id) => _repository.GetUser(id);
    public User GetUserEmail(string mail) => _repository.GetByEmail(mail);

    public int? Authenticate(UserLoginView userView)
    {
      var user = _repository.GetByLogin(userView.Login);
      if (user == null) return null;

      var isPasswordValid = ComputePasswordHash(user.Salt, userView.Password)
        .SequenceEqual(user.PasswordHash);

      return isPasswordValid ? user.Id : (int?)null;
    }

    public int Register(UserView userView)
    {
      var user = _mapper.Map<User>(userView);
      user.Salt = CreateSalt();
      user.PasswordHash = ComputePasswordHash(user.Salt, userView.Password);

      _repository.Create(user);
      return user.Id;
    }

    private byte[] CreateSalt()
    {
      var salt = new byte[AuthOptions.SaltSize];
      _secureRandom.GetBytes(salt);
      return salt;
    }

    private byte[] ComputePasswordHash(IEnumerable<byte> salt, string password)
    {
      return _sha1.ComputeHash(salt.Concat(Encoding.UTF8.GetBytes(password)).ToArray());
    }

    public bool HasUser(string login) => _repository.GetByLogin(login) != null;

    public string GenerateJWT(int userId)
    {
      var user = GetUser(userId);
      var credentials = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
        AuthOptions.Issuer,
        AuthOptions.Audience,
        new[]
        {
          new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
          new Claim(JwtRegisteredClaimNames.UniqueName, user.Login),
          new Claim(JwtRegisteredClaimNames.Email, user.Email)
        },
        expires: DateTime.UtcNow.AddSeconds(AuthOptions.Lifetime),
        signingCredentials: credentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
