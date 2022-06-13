using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DB_Test.EntityContext;
using DB_Test.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DB_Test.Pages
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Teacher")]
    public class InstitutesModel : PageModel
    {
        [Required(ErrorMessage = "Необходимо ввести название института")]
        public string? instituteName { get; set; }

        public readonly ApplicationContext _context;
        public List<Institutes> Institutes { get; set; }

        public InstitutesModel(ApplicationContext context)
        {
            _context = context;
           
        }

        public async Task<IActionResult> OnPostAddInst(string instituteName)
        {
            try
            {
                var institute = new Institutes { InstitutesName = instituteName };
                await _context.Institutes.AddAsync(institute);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                ModelState.AddModelError("", "Данный институт уже создан");

            }
            Institutes = await Task.Run(() => _context.Institutes.
                                    Where(_ => true).
                                    OrderBy(x => x.Id).ToList());

            return Page();
        }

        public async Task OnGetAsync()
        {
            Institutes = await Task.Run(() => _context.Institutes.
                                    Where(_ => true).
                                    OrderBy(x => x.Id).ToList());
        }

    }
}
