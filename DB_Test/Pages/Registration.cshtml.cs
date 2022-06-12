using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using DB_Test.Services;
using DB_Test.EntityContext;

namespace DB_Test.Pages
{
    [BindProperties]
    public class RegistrationModel : PageModel
    {
        [Required(ErrorMessage = "Необходимо указать почту")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Необходимо ввести пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Повторно введите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string? PasswordConfirm { get; set; }

        private readonly IAuthService _authService;
        private readonly ApplicationContext _db;

        public RegistrationModel(IAuthService authService, ApplicationContext db)
        {
            _authService = authService;
            _db = db;
        }

        public async Task<IActionResult> OnPostRegister()
        {
            var appUser = new Users { Email = Email, PwHash = _authService.GetHashedPassword(Password), Role = "Teacher" };
            try
            {
                await _db.Users.AddAsync(appUser);
                await _db.SaveChangesAsync();
                HttpContext.Session.SetString("Token", _authService.GetAuthData(Email, appUser));
                HttpContext.Session.SetString("userId", $"{appUser.Id}");
                HttpContext.Session.SetString("roles", $"{appUser.Role}");
                return RedirectToPage("StartPagecshtml");
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Неверные данные пользователя");
                return RedirectToPage("Registration");
            }
        }

        public void OnGet()
        {
        }
    }
}
