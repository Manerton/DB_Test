using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DB_Test.EntityContext;
using System.ComponentModel.DataAnnotations;
using DB_Test.Services;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace DB_Test.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        [Required(ErrorMessage = "Необходимо ввести почту")]
        [Display(Name = "Email")]
        public string? Email { get; set; } = "email";

        [Required(ErrorMessage = "Необходимо ввести пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; } = "word";

        private readonly IAuthService _authService;
        private readonly ApplicationContext _db;

        public IndexModel(IAuthService authService, ApplicationContext db)
        {
            _authService = authService;
            _db = db;
        }

        public async Task<IActionResult> OnPostLogin()
        {
            var appUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == Email);
            if (appUser != null && _authService.ValidateUserPassword(appUser.PwHash, Password))
            {
                HttpContext.Session.SetString("Token", _authService.GetAuthData(Email, appUser));
                HttpContext.Session.SetString("userId", $"{appUser.Id}");
                HttpContext.Session.SetString("roles", $"{appUser.Role}");
                if(appUser.Role == "Teacher")
                    return RedirectToPage("StartPagecshtml");
                else
                    return RedirectToPage("StartAdmin");
            }
            else
            {
                ModelState.AddModelError("","Неправильный логин или пароль");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostLogout()
        {
            var appUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == Email);
            HttpContext.Session.Clear();
            return RedirectToPage("Index");
        }
    }
}