using AutoMapper;
using questions_data.Entities;
using questions_view.ViewModels;

namespace questions_view
{
  public class MapperPr : Profile
  {
    public MapperPr()
    {
      CreateMap<User, UserView>().ReverseMap();
      CreateMap<QuestionView, Question>().ReverseMap();
      CreateMap<AnswerView, Answer>().ReverseMap();
    }
  }
}
