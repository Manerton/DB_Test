using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB_Test.EntityContext;
using DB_Test.Pages;

namespace DB_Test.Controllers
{
    public class DisciplinesController : Controller
    {
        public readonly ApplicationContext _db;

        public DisciplinesController(ApplicationContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddDiscipline(Disciplines disciplines)
        {
            try
            {
                await _db.Disciplines.AddAsync(disciplines);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            { }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditDiscipline(Disciplines disciplines)
        {
            var disciplineTemp = await _db.Disciplines.FirstOrDefaultAsync(x => x.Id == disciplines.Id);
            if (disciplineTemp == null)
                return Redirect("/Error");
            disciplineTemp.DisciplineName = disciplines.DisciplineName;
            _db.Disciplines.Update(disciplineTemp);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteDiscipline(int DisciplineId)
        {
            var discipline = await _db.Disciplines.FirstOrDefaultAsync(x => x.Id == DisciplineId);
            if(discipline == null)
                return Redirect("/Error");
            _db.Disciplines.Remove(discipline);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
