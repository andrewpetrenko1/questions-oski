using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace questions_data.Entities
{
  public class Question
  {
    public int Id { get; set; }
    public string QuestionText { get; set; }
    public int CorrectAnswerId { get; set; }
    public List<Answer> Answers { get; set; } = new List<Answer>();
  }
}
