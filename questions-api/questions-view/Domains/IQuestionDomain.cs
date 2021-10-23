using questions_view.ViewModels;
using System.Collections.Generic;

namespace questions_view.Domains
{
  public interface IQuestionDomain
  {
    IEnumerable<QuestionView> GetShuffledQuestions(int questionCount);
    IEnumerable<IEnumerable<QuestionView>> GetQuestionsList(int listSize, int questionCount);
  }
}
