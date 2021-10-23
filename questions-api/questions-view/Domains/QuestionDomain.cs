using AutoMapper;
using questions_data.Entities;
using questions_data.Repositories;
using questions_view.ViewModels;
using System.Collections.Generic;

namespace questions_view.Domains
{
  public class QuestionDomain : IQuestionDomain
  {
    private readonly IQuestionRepository _repository;
    private readonly IMapper _mapper;

    public QuestionDomain(IQuestionRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public IEnumerable<QuestionView> GetShuffledQuestions(int questionCount) => 
      _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionView>>(_repository.GetShuffledQuestions(questionCount));

    public IEnumerable<IEnumerable<QuestionView>> GetQuestionsList(int listSize, int questionCount)
    {
      var list = new List<IEnumerable<QuestionView>>();
      for (int i = 0; i < listSize; i++)
      {
        list.Add(GetShuffledQuestions(questionCount));
      }
      return list;
    }
  }
}
