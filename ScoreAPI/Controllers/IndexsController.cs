using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreAPI.ModelScore2;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IndexsController : ControllerBase
    {
        ScoreContext stc;
        public IndexsController(ScoreContext stc_in)
        {
            stc = stc_in;
        }
        [HttpGet]
        [Route("/Index/GetNumberofTeachers")]
        public IActionResult GetNumberofTeachers()
        {
            return Ok(new { data = stc.TblTeachers.Count() });
        }

        [HttpGet]
        [Route("/Index/GetNumberofStudents")]
        public IActionResult GetNumberofStudents()
        {
            return Ok(new { data = stc.TblStudents.Count() });
        }

    }
}
