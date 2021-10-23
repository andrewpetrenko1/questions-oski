using questions_data.Entities;
using System.Collections.Generic;

namespace questions_data.Repositories
{
  public interface IQuestionRepository
  {
    IEnumerable<Question> GetShuffledQuestions(int questionCount);
  }
}
