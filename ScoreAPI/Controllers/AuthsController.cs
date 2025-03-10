using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreAPI.ModelScore2;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        ScoreContext stc;
        public AuthsController(ScoreContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/Auths/GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            return Ok(new { data = stc.TblStudents.ToList() });
        }

        [HttpGet]
        [Route("/Auths/GetAllTeachers")]
        public IActionResult GetAllTeachers()
        {
            return Ok(new { data = stc.TblTeachers.ToList() });
        }

        [HttpGet]
        [Route("/Auths/GetAllAdmins")]
        public IActionResult GetAllAdmins()
        {
            return Ok(new { data = stc.TblAdmins.ToList() });
        }

        [HttpGet]
        [Route("/Auths/GetAllGrades")]
        public IActionResult GetAllGrades()
        {
            return Ok(new { data = stc.TblGrades.ToList() });
        }
    }
}
