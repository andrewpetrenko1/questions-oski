using System.Collections.Generic;

namespace questions_view.ViewModels
{
  public class QuestionView
  {
    public int Id { get; set; }
    public string QuestionText { get; set; }
    public int CorrectAnswerId { get; set; }
    public List<AnswerView> Answers { get; set; } = new List<AnswerView>();
  }
}
