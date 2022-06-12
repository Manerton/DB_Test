using DB_Test.EntityContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DB_Test.Pages
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Teacher")]
    public class TestsModel : PageModel
    {
        private readonly ApplicationContext _context;
        public List<SelectListItem> Test { get; set; } 
        public List<SelectListItem> Discipline { get; set; } 
        public List<SelectListItem> Questions { get; set; }
        public bool[] ListChecked { get; set; }

        public TestsModel(ApplicationContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Test = _context.Tests.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()}).ToList();

            Discipline = await Task.Run(() => _context.Disciplines.Select(z => new SelectListItem
                  {
                      Value = z.Id.ToString(),
                      Text = z.DisciplineName
                  }).ToList());

            Questions = await Task.Run(() => _context.Questions.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.TextQuestions
            }).ToList());
        }

        public async Task<IActionResult> OnPostCreate(string TestName, bool[] ListChecked, int disciplineId)
        {
            var QuestionsTemp = await Task.Run(() => _context.Questions.Where(_ => true).ToList());
            var TestTemp = new Tests { Id = Guid.NewGuid(), Name = TestName, CountQuestions = 0, DisciplineId = disciplineId };

            await _context.Tests.AddAsync(TestTemp);
            for (int i = 0; i < QuestionsTemp.Count(); ++i)
            {
                if (ListChecked[i])
                {
                    var testQuestion = new TestsQuestionsLink { QuestionId = QuestionsTemp[i].Id, TestId = TestTemp.Id };
                    await _context.TestsQuestionsLink.AddAsync(testQuestion);
                }
            }
            
            await _context.SaveChangesAsync();
            await CreateResultTests(TestTemp);
            return Redirect("/Tests");
        }

        private async Task CreateResultTests(Tests test)
        {
            List<Students> Students = await Task.Run(() => _context.Students.
                        Where(x => _context.GroupDisciplinesLinks.
                        Where(y => y.DisciplineId == test.DisciplineId).
                        Select(z => z.GroupId).Contains(x.GroupId)).ToList());

            List<Questions> QuestionsTemp = await Task.Run(() => _context.Questions.Where(x => _context.TestsQuestionsLink.Where(y => y.TestId == test.Id).Select(z => z.QuestionId).Contains(x.Id)).ToList());
            int MaxScore = 0;
            foreach(var questions in QuestionsTemp)
            {
                MaxScore += questions.Score;
            }
            Random random = new Random();
            foreach(var student in Students)
            {
                int sumScore = random.Next(0, MaxScore);
                ResultTests resultTests = new ResultTests { StudentId = student.Id, TestId = test.Id, SumScore = sumScore };
                await _context.ResultTests.AddAsync(resultTests);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> OnPostDelete(Guid TestId)
        {
            var TestTemp = _context.Tests.FirstOrDefault(x => x.Id == TestId);
            if (TestTemp == null)
                return Redirect("/Error");
            _context.Tests.Remove(TestTemp);
            await _context.SaveChangesAsync();
            return Redirect("/Tests");
        }
    }
}
