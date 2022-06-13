using DB_Test.EntityContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DB_Test.Pages
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Teacher")]
    public class DisciplinesModel : PageModel
    {
        [Required(ErrorMessage = "Необходимо ввести название дисциплины")]
        [Display(Name = "DisciplinName")]
        public string? DisciplinName { get; set; }

        public readonly ApplicationContext _context;
        
        public List<Disciplines> Disciplines { get; set; }

        public DisciplinesModel(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAddDisc(string DisciplinName)
        {
            try
            {
                var discipline = new Disciplines { DisciplineName = DisciplinName };
                await _context.Disciplines.AddAsync(discipline);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                ModelState.AddModelError("", "Данная дисциплина уже создана");

            }
            Disciplines = await Task.Run(() => _context.Disciplines.
                                    Where(_ => true).
                                    OrderBy(x => x.Id).ToList());

            return Page();
        }

        public async Task OnGetAsync()
        {
            Disciplines = await Task.Run(() => _context.Disciplines.
                                    Where(_ => true).
                                    OrderBy(x => x.Id).ToList());
        }
    }
}
