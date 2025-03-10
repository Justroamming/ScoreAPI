/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreAPI.ModelScore;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LessonClassesController : ControllerBase
    {
        ScoreTableContext stc;
        public LessonClassesController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/LessonClass/GetAllLessonClasses")]
        public IActionResult GetAllLessonClasses()
        {
            return Ok(new { data = stc.TblLessonClasses.ToList() });
        }

        [HttpGet]
        [Route("/LessonClass/GetLessonClassById")]
        public IActionResult GetLessonClassById(string id)
        {
            return Ok(new { data = stc.TblLessonClasses.Find(new Guid(id)) });
        }

        [HttpPost]
        [Route("/LessonClass/InsertLessonClass")]
        public IActionResult InsertLessonClass(string subjectID, string teacherID, DateTime lessondate, string location)
        {
            if (!Guid.TryParse(subjectID, out Guid parsedSubjectId))
            {
                return BadRequest(new { message = "Invalid GUID format for SubjectID." });
            }
            if (!Guid.TryParse(teacherID, out Guid parsedTeacherId))
            {
                return BadRequest(new { message = "Invalid GUID format for TeacherID." });
            }
            bool subjectExists = stc.TblSubjects.Any(sub => sub.SubjectId == parsedSubjectId);
            if (!subjectExists)
            {
                return NotFound(new { message = "Subject not found." });
            }
            bool teacherExists = stc.TblTeachers.Any(te => te.TeacherId == parsedTeacherId);
            if (!teacherExists)
            {
                return NotFound(new { message = "Teacher not found." });
            }

            TblLessonClass lc = new TblLessonClass
            {
                LessonClassId = Guid.NewGuid(),
                SubjectId = parsedSubjectId,
                TeacherId = parsedTeacherId,
                LessonDate = lessondate,
                Location = location

            };
            try
            {
                stc.TblLessonClasses.Add(lc);
                stc.SaveChanges();
                return Ok(new { message = "Lesson Class added successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("/LessonClass/UpdateLessonClass")]
        public IActionResult UpdateLessonClass(string id, string subjectID, string teacherID, DateTime lessondate, string location)
        {
            if(!Guid.TryParse(id, out Guid parsedLessonClassId))
            {
                return BadRequest(new { message = "Invalid GUID format for LessonClassID." });
            }
            if (!Guid.TryParse(subjectID, out Guid parsedSubjectId))
            {
                return BadRequest(new { message = "Invalid GUID format for SubjectID." });
            }
            if (!Guid.TryParse(teacherID, out Guid parsedTeacherId))
            {
                return BadRequest(new { message = "Invalid GUID format for TeacherID." });
            }

            TblLessonClass? lc = stc.TblLessonClasses.Find(parsedLessonClassId);
            if (lc == null)
            {
                return NotFound(new { message = "Lesson Class not found." });
            }
            bool subjectExists = stc.TblSubjects.Any(sub => sub.SubjectId == parsedSubjectId);
            if (!subjectExists)
            {
                return NotFound(new { message = "Subject not found." });
            }
            bool teacherExists = stc.TblTeachers.Any(te => te.TeacherId == parsedTeacherId);
            if (!teacherExists)
            {
                return NotFound(new { message = "Teacher not found." });
            }

            lc.SubjectId = parsedSubjectId;
            lc.TeacherId = parsedTeacherId;
            lc.LessonDate = lessondate;
            lc.Location = location;

            try
            {
                stc.TblLessonClasses.Update(lc);
                stc.SaveChanges();
                return Ok(new { message = "Lesson Class updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("/LessonClass/DeleteLessonClass")]
        public IActionResult DeleteLessonClass(string id)
        {
            if(!Guid.TryParse(id, out Guid parsedLessonClassId))
            {
                return BadRequest(new { message = "Invalid GUID format for LessonClassID." });
            }


            TblLessonClass? lc = stc.TblLessonClasses.Find(parsedLessonClassId);
            if (lc == null)
            {
                return NotFound(new { message = "Lesson Class not found." });
            }
            try
            {
                stc.TblLessonClasses.Remove(lc);
                stc.SaveChanges();
                return Ok(new { message = "Lesson Class deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
*/