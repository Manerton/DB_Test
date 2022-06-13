using DB_Test.DTO;
using DB_Test.EntityContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DB_Test.Pages
{
    public class ResultTestAdminModel : PageModel
    {
        private readonly ApplicationContext _context;

        public int SelectedGroupId { get; set; }
        public Guid SelectedTestId { get; set; }
        public List<ResultTestDTO> ResultTest = new List<ResultTestDTO>();
        public List<SelectListItem> Groups { get; set; }
        public List<SelectListItem> Tests { get; set; }

        public ResultTestAdminModel(ApplicationContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int? groupId, Guid? testId)
        {
            Groups = await Task.Run(() => _context.Groups.Select(x => new SelectListItem { Text = x.GroupName, Value = x.Id.ToString() }).ToList());
            Tests = await Task.Run(() => _context.Tests.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList());

            if (Groups.Count != 0 && Tests.Count != 0)
            {

                if (groupId == null)
                    SelectedGroupId = _context.Groups.First().Id;
                else
                    SelectedGroupId = groupId.Value;
                if (testId == null)
                    SelectedTestId = _context.Tests.First().Id;
                else
                    SelectedTestId = testId.Value;

                var selectGroup = Groups.Find(x => int.Parse(x.Value) == SelectedGroupId);
                selectGroup.Selected = true;

                Tests = await Task.Run(() => _context.Tests.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList());
                var selectTest = Tests.Find(x => Guid.Parse(x.Value) == SelectedTestId);
                selectTest.Selected = true;

                var pageObject = (from student in _context.Students
                                  join resultTest in _context.ResultTests on student.Id equals resultTest.StudentId
                                  where resultTest.TestId == SelectedTestId && student.GroupId == SelectedGroupId
                                  select new { resultTest, student }).ToList();

                foreach (var temp in pageObject)
                {
                    ResultTest.Add(new ResultTestDTO { IdResult = temp.resultTest.Id, StudentName = temp.student.StudentName, SumScore = temp.resultTest.SumScore });
                }
            }
        }

        public async Task<IActionResult> OnPostDelete(int resultId)
        {
            var resultTemp = _context.ResultTests.FirstOrDefault(x => x.Id == resultId);
            if (resultTemp == null)
                return Redirect("/Error");
            _context.ResultTests.Remove(resultTemp);
            await _context.SaveChangesAsync();
            return Redirect("/ResultTestAdmin");
        }
    }
}
