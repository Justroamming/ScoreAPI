/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreAPI.ModelScore;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        ScoreTableContext stc;
        public TeachersController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/Teacher/GetAllTeacher")]
        public IActionResult GetAllTeacher()
        {
            return Ok(new { data = stc.TblTeachers.ToList() });
        }

        [HttpGet]
        [Route("/Teacher/GetTeacherById")]
        public IActionResult GetTeacherById(string id)
        {
            return Ok(new { data = stc.TblTeachers.Find(new Guid(id)) });
        }

        [HttpGet]
        [Route("/Teacher/GetNumberofTeachers")]
        public IActionResult GetNumberofTeachers()
        {
            return Ok(new { data = stc.TblTeachers.Count() });
        }


        [HttpPost]
        [Route("/Teacher/InsertTeacher")]

        public IActionResult InsertTeacher(string FName, string LName, string email, string password)
        {
            TblTeacher te = new TblTeacher();
            te.TeacherId = System.Guid.NewGuid();
            te.FirstName = FName;
            te.LastName = LName;
            te.Email = email;
            te.Password = password;
            
            stc.TblTeachers.Add(te);
            stc.SaveChanges();
            return Ok(new { te });

        }

        [HttpPut]
        [Route("/Teacher/UpdateTeacher")]

        public IActionResult UpdateTeacher(string id, string FName, string LName, string email, string password)
        {
            TblTeacher te = new TblTeacher();
            te.TeacherId = new Guid(id);
            te.FirstName = FName;
            te.LastName = LName;
            te.Email = email;
            te.Password = password;
            stc.TblTeachers.Update(te);
            stc.SaveChanges();
            return Ok(new { te });

        }

        [HttpDelete]
        [Route("/Teacher/DeleteTeacher")]

        public IActionResult DeleteTeacher(string id)
        {
            TblTeacher te = new TblTeacher();
            te.TeacherId = new Guid(id);
            stc.TblTeachers.Remove(te);
            stc.SaveChanges();
            return Ok(new { te });

        }
    }
}
*/