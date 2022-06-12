using DB_Test.EntityContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DB_Test.Pages
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Teacher")]
    public class QuestionsModel : PageModel
    {
        readonly public ApplicationContext _context;

        public List <Questions> Questions { get; set; }

        public QuestionsModel(ApplicationContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Questions = await Task.Run(() => _context.Questions.
                                    Where(_ => true).
                                    OrderBy(x => x.Id).ToList());
        }
    }
}
