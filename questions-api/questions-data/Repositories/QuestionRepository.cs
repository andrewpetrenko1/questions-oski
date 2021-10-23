using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using questions_data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace questions_data.Repositories
{
  public class QuestionRepository : IQuestionRepository
  {
    private readonly IquestionsDbContext _context;
    public QuestionRepository(IquestionsDbContext context) { 
      _context = context;

      if (!_context.Questions.Any())
      {
        GenerateQuestionsAsync(50).Wait();
      }
    }

    public IEnumerable<Question> GetShuffledQuestions(int questionCount)
    {
      var rnd = new Random();
      return _context.Questions.Include(q => q.Answers.OrderBy(a => a.TextAnswer))
        .ToList().OrderBy(q => rnd.Next()).Take(questionCount);
    }

    private async Task GenerateQuestionsAsync(int count)
    {
      var client = new HttpClient();
      HttpResponseMessage response = await client.GetAsync($"https://opentdb.com/api.php?amount={count}");
      response.EnsureSuccessStatusCode();
      string responseBody = await response.Content.ReadAsStringAsync();

      dynamic results = JToken.Parse(responseBody)["results"];

      foreach (var res in results)
      {
        var question = new Question
        {
          Id = 0,
          Answers = { },
          CorrectAnswerId = 0,
          QuestionText = res.question
        };

        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();

        var correctAnswer = new Answer { Id = 0, TextAnswer = res.correct_answer, QuestionId = question.Id };
        await _context.Answers.AddAsync(correctAnswer);
        await _context.SaveChangesAsync();

        var editQ = await _context.Questions.FindAsync(question.Id);
        editQ.CorrectAnswerId = correctAnswer.Id;

        foreach (string answ in res.incorrect_answers)
        {
          await _context.Answers.AddAsync(new Answer { Id = 0, TextAnswer = answ, QuestionId = question.Id });
        }
        await _context.SaveChangesAsync();
      }
    }
  }
}
