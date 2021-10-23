using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace questions_data.Entities
{
  public class Answer
  {
    public int Id { get; set; }
    public string TextAnswer { get; set; }
    public int QuestionId { get; set; }
    public virtual Question Question { get; set; }
  }
}
