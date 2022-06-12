using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB_Test.EntityContext;
using DB_Test.Pages;

namespace DB_Test.Controllers
{
    public class InstitutesController : Controller
    {
        public readonly ApplicationContext _db;

        public InstitutesController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetInstitutes()
        {
           return Ok(await Task.Run(() => _db.Institutes.ToList()));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["msg"] = "Данный институт уже создан";
            return View("/Institutes");
        }

        [HttpPost]
        public async Task<IActionResult> AddInstitute(Institutes institute)
        {
            try
            {
                await _db.Institutes.AddAsync(institute);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                ViewData["msg"] = "Данный институт уже создан";
                return View("Данный институт уже создан");

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditInstitute(Institutes institute)
        {
            var instituteTemp = await _db.Institutes.FirstOrDefaultAsync(i => i.Id == institute.Id);
            if(instituteTemp == null)
                return Redirect("/Error");
            instituteTemp.InstitutesName = institute.InstitutesName;
            _db.Institutes.Update(instituteTemp);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteInstitute(int instituteId)
        {
            var institute = await _db.Institutes.FirstOrDefaultAsync(i => i.Id == instituteId);
            if (institute == null)
                return Redirect("/Error");
            _db.Institutes.Remove(institute);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> InstitutesList(int instituteId)
        {
            var institutes = await _db.Institutes.FirstOrDefaultAsync(i => i.Id == instituteId);    
            if(institutes != null)
                return PartialView("/InstitutesList" ,institutes);
            return View("/Error");
        }
    }
}
