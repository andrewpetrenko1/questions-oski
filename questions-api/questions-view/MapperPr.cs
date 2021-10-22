using AutoMapper;
using questions_data.Entities;
using questions_view.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace questions_view
{
  public class MapperPr : Profile
  {
    public MapperPr()
    {
      CreateMap<User, UserView>().ReverseMap();
    }
  }
}
