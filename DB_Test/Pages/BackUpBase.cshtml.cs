using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.ComponentModel.DataAnnotations;
using DB_Test.Extend;

namespace DB_Test.Pages
{
    public class BackUpBaseModel : PageModel
    {
        [Display(Name = "Путь к резервной копии")]
        public string? inFile { get; set; }
        [Display(Name = "Название резервной копии")]
        public string? outFile { get; set; }
        private IConfiguration _configuration { get; }

        public BackUpBaseModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnPostRestore(string inFile)
        {
            BackUpRestore backUpRestore = new BackUpRestore();
            ModelState.AddModelError("", backUpRestore.Restore(_configuration, inFile));
        }

        public void OnPostBackUp(string outFile)
        {
            BackUpRestore backUpRestore = new BackUpRestore();
            ModelState.AddModelError("", backUpRestore.BackUp(_configuration, outFile));
        }

        public void OnGet()
        {
        }
    }
}
