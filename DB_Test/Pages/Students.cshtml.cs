using DB_Test.EntityContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DB_Test.Pages
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Teacher")]
    public class StudentsModel : PageModel
    {
        private readonly ApplicationContext _context;
        public int SeletedGroupId;

        public List<SelectListItem> Groups;
        public List<Students> Students;

        public StudentsModel(ApplicationContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int? GroupId)
        {
            if (GroupId == null)
            {
                var Temp = _context.Groups.FirstOrDefault();
                if (Temp == null)
                    SeletedGroupId = -1;
                else
                    SeletedGroupId = Temp.Id;
            }
            else
                SeletedGroupId = GroupId.Value;

            if (SeletedGroupId > 0)
            {
                Groups = await Task.Run(() => _context.Groups.Select(x => new SelectListItem
                { Value = x.Id.ToString(), Text = x.GroupName }).ToList());
                var selectGr = Groups.Find(x => int.Parse(x.Value) == SeletedGroupId);
                selectGr.Selected = true;
            }
            else
                Groups = new List<SelectListItem>();

            Students = await Task.Run(() => _context.Students.
                            Where(x => x.GroupId == SeletedGroupId).OrderBy(x => x.Id).ToList());

           

        }
    }
}
