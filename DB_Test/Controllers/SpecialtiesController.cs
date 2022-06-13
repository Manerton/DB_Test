using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB_Test.EntityContext;
using DB_Test.Pages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DB_Test.Controllers
{
    public class SpecialtiesController : Controller
    {
        public readonly ApplicationContext _db;
 
        public SpecialtiesController(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(int InstituteId)
        {
            return View(new SpecialtiesModel(_db));
        }

        [HttpPost]
        public async Task<IActionResult> AddSpecialtie(Specialties specialties)
        {
            try
            {
                await _db.Specialties.AddAsync(specialties);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
            return Redirect("/Specialties?InstituteId="+ specialties.InstituteId);
            //return RedirectToAction("Index", "Specialties", specialties.InstituteId);
        }

        public async Task<IActionResult> EditSpecialtie(Specialties specialties)
        {
            var specialtiesTemp = await _db.Specialties.FirstOrDefaultAsync(x => x.Id == specialties.Id);
            if (specialtiesTemp == null)
                return Redirect("/Error");
            specialtiesTemp.SpecialtiesName = specialties.SpecialtiesName;
            specialtiesTemp.InstituteId = specialties.InstituteId;
            _db.Specialties.Update(specialtiesTemp);
            await _db.SaveChangesAsync();
            return Redirect("/Specialties?InstituteId=" + specialties.InstituteId);
        }

        public async Task<IActionResult> DeleteSpecialtie(int SpecialtieId)
        {
            var specialties = await _db.Specialties.FirstOrDefaultAsync(x => x.Id == SpecialtieId);
            if (specialties == null)
                return Redirect("/Error");
            _db.Specialties.Remove(specialties);
            await _db.SaveChangesAsync();
            return Redirect("/Specialties?InstituteId=" + specialties.InstituteId);
        }
    }
}
