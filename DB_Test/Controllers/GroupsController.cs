using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB_Test.EntityContext;
using DB_Test.Pages;


namespace DB_Test.Controllers
{
    public class GroupsController : Controller
    {
        public readonly ApplicationContext _db;

        public GroupsController(ApplicationContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? selectedDisciplineId)
        {
            return View();
        }

        public async Task<IActionResult> AddGroup(Groups groups)
        {
            await _db.Groups.AddRangeAsync(groups);
            await _db.SaveChangesAsync();
            return Redirect("/Groups?SpecialtieId=" + groups.SpecialtieId);
        }

        public async Task<IActionResult> EditGroup(Groups groups)
        {
            var groupTemp = await _db.Groups.FirstOrDefaultAsync(x => x.Id == groups.Id);
            if(groupTemp == null)
                return Redirect("/Error");
            groupTemp.GroupName = groups.GroupName;
            groupTemp.SpecialtieId = groups.SpecialtieId;
            _db.Groups.Update(groupTemp);
            await _db.SaveChangesAsync();
            return Redirect("/Groups?SpecialtieId=" + groups.SpecialtieId);
        }

        public async Task<IActionResult> DeleteGroup(int GroupId)
        {
            var groups = await _db.Groups.FirstOrDefaultAsync(x => x.Id == GroupId);
            if (groups == null)
                return Redirect("/Error");
            _db.Groups.Remove(groups);
            await _db.SaveChangesAsync();
            return Redirect("/Groups?SpecialtieId=" + groups.SpecialtieId);
        }
    }
}
