using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DB_Test.EntityContext;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace DB_Test.Pages
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Teacher")]
    public class SpecialtiesModel : PageModel
    {
        public readonly ApplicationContext _context;

        public int Id { get; set; }

        public List<Specialties> Specialties { get; set; }
        public List<SelectListItem> Institutes { get; set; }

        public SpecialtiesModel(ApplicationContext context)
        {
            _context = context;
        }


        public async Task OnGetAsync(int? InstituteId)
        {
            Institutes = await Task.Run(() => _context.Institutes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.InstitutesName
            }).ToList());

            if (Institutes.Count != 0)
            {

                if (InstituteId == null)
                    Id = _context.Institutes.First().Id;
                else
                    Id = InstituteId.Value;

                Specialties = await Task.Run(() => _context.Specialties.
                                         Where(x => x.InstituteId == Id).
                                         OrderBy(x => x.Id).ToList());

                var instititeId = int.Parse(Institutes[0].Value);
                if (InstituteId != null)
                {
                    var selectGr = Institutes.Find(x => int.Parse(x.Value) == InstituteId);
                    selectGr.Selected = true;
                    instititeId = InstituteId.Value;
                }
            }
        }

    }
}
