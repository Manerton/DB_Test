using Microsoft.AspNetCore.Mvc;
using DB_Test.EntityContext;
using Microsoft.EntityFrameworkCore;

namespace DB_Test.Controllers
{
    public class TestsController : Controller
    {
        public readonly ApplicationContext _db;

        public TestsController(ApplicationContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddTest(Tests tests)
        {
            await _db.Tests.AddAsync(tests);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditTest(Tests tests)
        {
            var testTemp = await _db.Tests.FirstOrDefaultAsync(x => x.Id == tests.Id);
            if (testTemp == null)
                return Redirect("/Error");
            testTemp.Name = tests.Name;
            testTemp.DisciplineId = tests.DisciplineId;
            _db.Tests.Update(testTemp);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteTest(Guid TestId)
        {
            var tests = await _db.Tests.FirstOrDefaultAsync(x => x.Id == TestId);
            if(tests == null)
                return Redirect("/Error");
            _db.Tests.Remove(tests);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
