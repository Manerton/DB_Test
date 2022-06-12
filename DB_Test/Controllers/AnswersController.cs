using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB_Test.EntityContext;

namespace DB_Test.Controllers
{
    public class AnswersController : Controller
    {
        public readonly ApplicationContext _db;

        public AnswersController(ApplicationContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllVariantsById(Guid QuestionId)
        {
            return Ok(await Task.Run(() => _db.Answers.Where(x => x.QuestionsId == QuestionId).ToList()));
        }

        public async Task<IActionResult> AddAnswer(Answers answers)
        {
            await _db.Answers.AddAsync(answers);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAnswer(Answers answers)
        {
            var answerTemp = await _db.Answers.FirstOrDefaultAsync(x => x.Id == answers.Id);
            if (answerTemp == null)
                return Redirect("/Error");
            answerTemp = answers;
            _db.Answers.Update(answerTemp);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAnswer(int AnswerId)
        {
            var answer = await _db.Answers.FirstOrDefaultAsync(x => x.Id == AnswerId);
            if (answer == null)
                return Redirect("/Error");
            _db.Answers.Remove(answer);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
