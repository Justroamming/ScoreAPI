/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreAPI.ModelScore;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        ScoreTableContext stc;
        public SubjectsController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/Subject/GetAllSubjects")]
        public IActionResult GetAllSubject()
        {
            return Ok(new { data = stc.TblSubjects.ToList() });
        }


        [HttpPost]
        [Route("/Subject/InsertSubject")]
        public IActionResult InsertSubject(string SName)
        {
            TblSubject su = new TblSubject();
            su.SubjectId = System.Guid.NewGuid();
            su.SubjectName = SName;
          

            stc.TblSubjects.Add(su);
            stc.SaveChanges();
            return Ok(new { su });

        }

        [HttpPut]
        [Route("/Subject/UpdateSubject")]
        public IActionResult UpdateSubject(string id, string SName)
        {
            TblSubject su = new TblSubject();
            su.SubjectId = new Guid(id);
            su.SubjectName = SName;

            stc.TblSubjects.Update(su);
            stc.SaveChanges();
            return Ok(new { su });
        }

        [HttpDelete]
        [Route("/Subject/DeleteSubject")]
        public IActionResult DeleteSubject(string id)
        {
            TblSubject su = new TblSubject();
            su.SubjectId = new Guid(id);

            stc.TblSubjects.Remove(su);
            stc.SaveChanges();

            return Ok(new { su });
        }
    }
}
*/