using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace questions_view
{
  public class AuthOptions
  {
    public const string Issuer = "QuestionsApi";
    public const string Audience = "QuestionsFront";
    const string SecretKey = "xecretKeywqejane";
    public const int Lifetime = 3000;
    public const int SaltSize = 5;
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
      return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
    }
  }
}
