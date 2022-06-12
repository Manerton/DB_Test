using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB_Test.EntityContext;

namespace DB_Test.Controllers
{
    public class StudentsController : Controller
    {
        public readonly ApplicationContext _db; 

        public StudentsController(ApplicationContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddStudent(Students students)
        {
            await _db.Students.AddAsync(students);
            await _db.SaveChangesAsync();
            return Redirect("/Students?GroupId=" + students.GroupId);
        }

        public async Task<IActionResult> EditStudent(Students students)
        {
            var studentTemp = await _db.Students.FirstOrDefaultAsync(x => x.Id == students.Id);
            if (studentTemp == null)
                return Redirect("/Error");
            studentTemp.StudentName = students.StudentName;
            studentTemp.GroupId = students.GroupId;
            _db.Students.Update(studentTemp);
            await _db.SaveChangesAsync();
            return Redirect("/Students?GroupId=" + students.GroupId);
        }

        public async Task<IActionResult> DeleteStudent(int StudentId)
        {
            var student = await _db.Students.FirstOrDefaultAsync(x => x.Id == StudentId);
            if (student == null)
                return Redirect("/Error");
            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
            return Redirect("/Students?GroupId=" + student.GroupId);
        }

    }
}
