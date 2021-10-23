using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using questions_data.Entities;
using questions_view.Domains;
using questions_view.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace questions_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class QuestionController : ControllerBase
  {
    private readonly IQuestionDomain _domain;
    public QuestionController(IQuestionDomain domain) => _domain = domain;

    [HttpGet("{count}")]
    public IActionResult GetQuestions(int count) => Ok(_domain.GetShuffledQuestions(count));

    [HttpGet("list_size={listSize}&quest_count={questCount}")]
    public IActionResult GetQuestionsList(int listSize, int questCount) => Ok(_domain.GetQuestionsList(listSize, questCount));
  }
}
