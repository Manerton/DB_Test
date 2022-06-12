using DB_Test.EntityContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DB_Test.Pages
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Teacher")]
    public class GroupsModel : PageModel
    {
        readonly public ApplicationContext _context;
        public int SelectedInstituteId;
        public int SelectedSpecialiteId;

        public List<Groups> Groups { get; set; }
        public List<SelectListItem> Specialties = new List<SelectListItem>();
        public List<SelectListItem> Institutes { get; set; }
        public GroupsModel(ApplicationContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int? InstituteId, int? SpecialtieId)
        {
            Institutes = await Task.Run(() => _context.Institutes.Select(x =>
                           new SelectListItem
                           {
                               Value = x.Id.ToString(),
                               Text = x.InstitutesName
                           }).ToList());

            if (Institutes.Count != 0)
            {

                if (InstituteId == null)
                {
                    if (SpecialtieId != null)
                        SelectedInstituteId = _context.Specialties.Where(x => x.Id == SpecialtieId).First().InstituteId;
                    else
                        SelectedInstituteId = _context.Institutes.First().Id;
                }
                else
                    SelectedInstituteId = InstituteId.Value;


                var selectInst = Institutes.Find(x => int.Parse(x.Value) == SelectedInstituteId);
                selectInst.Selected = true;

                if (SpecialtieId == null)
                    SelectedSpecialiteId = _context.Specialties.First(x => x.InstituteId == SelectedInstituteId).Id;
                else
                    SelectedSpecialiteId = SpecialtieId.Value;

                Specialties = await Task.Run(() => _context.Specialties.
                                            Where(x => x.InstituteId == SelectedInstituteId).
                                            Select(x => new SelectListItem
                                            {
                                                Value = x.Id.ToString(),
                                                Text = x.SpecialtiesName
                                            }).ToList());
                if (Specialties.Count != 0)
                {

                    var selectSpec = Specialties.Find(x => int.Parse(x.Value) == SelectedSpecialiteId);
                    selectSpec.Selected = true;


                    Groups = await Task.Run(() => _context.Groups.
                                         Where(x => x.SpecialtieId == SelectedSpecialiteId).
                                        OrderBy(x => x.Id).ToList());
                }
            }
        }
    }
}
