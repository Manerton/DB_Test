using DB_Test.EntityContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DB_Test.Pages
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Teacher")]
    public class GroupDisciplineLinkModel : PageModel
    {
        private readonly ApplicationContext _context;

        public int SelectedGroupId { get; set; }
        public bool IsSelected { get; set; }
        public List<SelectListItem> Groups { get; set; }
        public List<SelectListItem> Disciplines { get; set; }
        public List<SelectListItem> GroupDiscipline { get; set; }
        public bool []ListChecked { get; set; }
        public int []IdChecked { get; set; }


        public GroupDisciplineLinkModel(ApplicationContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int? GroupId, string? Type)
        {

            Groups = await Task.Run(() => _context.Groups.Select(x =>
                           new SelectListItem
                           {
                               Value = x.Id.ToString(),
                               Text = x.GroupName
                           }).ToList());

            if (Groups.Count != 0)
            {
                if (GroupId == null)
                    SelectedGroupId = await Task.Run(() => _context.Groups.First().Id);
                else
                    SelectedGroupId = GroupId.Value;

                GroupDiscipline = await Task.Run(() => _context.Disciplines.
                      Where(x => _context.GroupDisciplinesLinks.Where(y => y.GroupId == SelectedGroupId).
                      Select(z => z.DisciplineId).Contains(x.Id)).Select(z => new SelectListItem
                      {
                          Value = z.Id.ToString(),
                          Text = z.DisciplineName
                      }).ToList());

                if (Type == null)
                {
                    IsSelected = false;
                }
                else
                {
                    IsSelected = true;
                    Disciplines = await Task.Run(() => _context.Disciplines.Select(x =>
                                           new SelectListItem
                                           {
                                               Value = x.Id.ToString(),
                                               Text = x.DisciplineName
                                           }).ToList());

                    var temp = new List<SelectListItem>();
                    for (int i = 0; i < Disciplines.Count(); ++i)
                    {
                        if (!GroupDiscipline.Any(x => x.Text == Disciplines[i].Text))
                        {
                            temp.Add(Disciplines[i]);
                        }
                    }
                    Disciplines = temp;
                }
                var selectGroup = Groups.Find(x => int.Parse(x.Value) == SelectedGroupId);
                selectGroup.Selected = true;
            }
            else
            {
                GroupDiscipline = new List<SelectListItem>();
            } 
                
        }

        public async Task<IActionResult> OnPostChecked(bool[] ListChecked, int SelectedGroupId, int[] IdChecked)
        {
            for (int i = 0; i < ListChecked.Count(); ++i)
            {
                if (ListChecked[i])
                {
                    await _context.GroupDisciplinesLinks.AddAsync(new EntityContext.GroupDisciplinesLink 
                    { GroupId = SelectedGroupId, DisciplineId = IdChecked[i] });
                }
            }   
            await _context.SaveChangesAsync();
            return Redirect("/GroupDisciplineLink?GroupId="+ SelectedGroupId.ToString());
        }

        public async Task<IActionResult> OnPostDelete(int SelectedGroupId, int DisciplineId)
        {
            var groupDiscipline = await Task.Run(() => _context.GroupDisciplinesLinks.FirstOrDefault(x => x.GroupId == SelectedGroupId && x.DisciplineId == DisciplineId));
            if (groupDiscipline == null)
                return Redirect("/Error");
            _context.Remove(groupDiscipline);
            await _context.SaveChangesAsync();
            return Redirect("/GroupDisciplineLink?GroupId=" + SelectedGroupId.ToString());
        }
    }
}
