using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreAPI.ModelScore2;

namespace ScoreAPI.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Route("[controller]")]
    [ApiController]
    public class ProfileTeachersController : ControllerBase
    {
        ScoreContext stc;
        public ProfileTeachersController(ScoreContext stc_in)
        {
            stc = stc_in;
        }

        [HttpPut]
        [Route("/ProfileTeachers/UpdateTeacherPassword")]
        public IActionResult UpdateTeachersPassword(string id, string password)
        {
            if (!Guid.TryParse(id, out Guid parsedTeachersId))
            {
                return BadRequest(new { message = "Invalid Teachers ID format." });
            }
            TblTeacher? te = stc.TblTeachers.Find(parsedTeachersId);
            if ((te == null))
            {
                return NotFound(new { message = "Teachers not found." });
            }
            te.Password = password;
            try
            {
                stc.TblTeachers.Update(te);
                stc.SaveChanges();
                return Ok(new { te });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the Teachers.", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("/ProfileTeachers/GetTeacherById")]
        public IActionResult GeTeacherById(string id)
        {
            return Ok(new { data = stc.TblTeachers.Find(new Guid(id)) });
        }
    }
}
