using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using DB_Test.EntityContext;
using DB_Test.DTO;

namespace DB_Test.Controllers
{
    public class QuestionsController : Controller
    {
        public readonly ApplicationContext _db;

        public QuestionsController(ApplicationContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionById(Guid QuestionId)
        {
            return Ok(await Task.Run(() => _db.Questions.Where(x => x.Id == QuestionId).FirstOrDefault()));
        }

        public async Task<IActionResult> AddQuestion([FromBody] QuestionsDTO questions)
        {
            var questionsMain =
                new Questions {Id = Guid.NewGuid(), TextQuestions = questions.TextQuestions, TrueAnswer = questions.TrueAnswer, Score = questions.Scores, Theme = questions.Theme};
            await _db.Questions.AddAsync(questionsMain);
            foreach (var answer in questions.Answers)
            {
                var ans = new Answers();
                ans.Text = answer;
                ans.QuestionsId = questionsMain.Id;
                await _db.Answers.AddAsync(ans);
            }
            await _db.SaveChangesAsync();
            return Ok(questions);
            //return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditQuestion(Questions questions)
        {
            var questionTemp = await _db.Questions.FirstOrDefaultAsync(x => x.Id == questions.Id);
            if (questionTemp == null)
                return Redirect("/Error");
            List<Answers> answerTemp = await Task.Run(() => _db.Answers.Where(x => x.QuestionsId == questions.Id).ToList());
         
            questionTemp = questions;
            _db.Questions.Update(questionTemp);
            await _db.SaveChangesAsync();
            return Ok(questions);
        }
        
        public async Task<IActionResult> DeleteQuestion(Guid QuestionId)
        {
            var question = await _db.Questions.FirstOrDefaultAsync(x => x.Id == QuestionId);
            if (question == null)
                return Redirect("/Error");
            _db.Questions.Remove(question);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
