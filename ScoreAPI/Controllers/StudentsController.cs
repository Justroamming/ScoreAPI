/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreAPI.ModelScore;
using System.Formats.Asn1;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        ScoreTableContext stc;
        public StudentsController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/Student/GetAllStudents")]
        public IActionResult GetAllAdmins()
        {
            return Ok(new { data = stc.TblStudents.ToList() });
        }

        [HttpGet]
        [Route("/Student/GetStudentById")]
        public IActionResult GetStudentById(string id)
        {
            return Ok(new { data = stc.TblStudents.Find(new Guid(id)) });
        }

        [HttpGet]
        [Route("/Student/GetNumberofStudents")]
        public IActionResult GetNumberofStudents()
        {
            return Ok(new { data = stc.TblStudents.Count() });
        }

        [HttpGet]
        [Route("Student/GetAllGradesOfAStudent")]
        public async Task<IActionResult> GetAllGradesOfAStudent(Guid id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetStudentGrades @StudentID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@StudentID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var grades = new List<object>();

                            while (await reader.ReadAsync())
                            {
                                grades.Add(new
                                {
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Score = Convert.ToDouble(reader["Score"]),
                                    GradeDate = Convert.ToDateTime(reader["GradeDate"]),
                                    TestType = reader["TestType"].ToString(),
                                    SubjectName = reader["SubjectName"].ToString()
                                });
                            }

                            return Ok(grades);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching student grades.", error = ex.Message });
            }
        }



        [HttpPost]
        [Route("/Student/InsertStudent")]

        public IActionResult InsertStudent(string FName, string LName, string email, string password, string cohortID)
        {
            if (!Guid.TryParse(cohortID,out Guid parsedCohortId))
            {
                return BadRequest(new { message = "Invalid GUID format for TeacherID." });
            }

            bool cohortExists = stc.TblCohorts.Any(co => co.CohortId == parsedCohortId);
            if (!cohortExists) {
                return NotFound(new { message = "Cohort not found." });
            }

            TblStudent st = new TblStudent
            {
                StudentId = Guid.NewGuid(),
                FirstName = FName,
                LastName = LName,
                Email = email,
                Password = password,
                CohortId = parsedCohortId


            };
            try
            {
                stc.TblStudents.Add(st);
                stc.SaveChanges();
                return Ok(new { st });
            }
            catch (Exception ex) {
                return StatusCode(500, new { message = "An error occurred while inserting the student.", error = ex.Message });
            }
        }

        [HttpPut]
        [Route("/Student/UpdateStudent")]

        public IActionResult UpdateStudent(string id, string FName, string LName, string email, string password, string cohortID)
        {
            if(!Guid.TryParse(id, out Guid parsedStudentId))
            {
                return BadRequest(new { message = "Invalid Student ID format." });
            }

            if (!Guid.TryParse(cohortID, out Guid parsedCohortId))
            {
                return BadRequest(new { message = "Invalid GUID format for TeacherID." });
            }


            TblStudent? st = stc.TblStudents.Find(parsedStudentId);
            if ((st==null))
            {
                return NotFound(new { message = "Student not found." });
            }

            bool cohortExists = stc.TblCohorts.Any(co => co.CohortId == parsedCohortId);
            if (!cohortExists)
            {
                return NotFound(new { message = "Cohort not found." });
            }
                
            st.FirstName = FName;
            st.LastName = LName;
            st.Email = email;
            st.Password = password;
            st.CohortId = parsedCohortId;
            
            try
            {
                stc.TblStudents.Update(st);
                stc.SaveChanges();
                return Ok(new { st });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while inserting the student.", error = ex.Message });
            }

        }

        [HttpPut]
        [Route("/Student/UpdateStudentPassword")]
        public IActionResult UpdateStudentPassword(string id, string password)
        {
            if (!Guid.TryParse(id, out Guid parsedStudentId))
            {
                return BadRequest(new { message = "Invalid Student ID format." });
            }
            TblStudent? st = stc.TblStudents.Find(parsedStudentId);
            if ((st == null))
            {
                return NotFound(new { message = "Student not found." });
            }
            st.Password = password;
            try
            {
                stc.TblStudents.Update(st);
                stc.SaveChanges();
                return Ok(new { st });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while inserting the student.", error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("/Student/DeleteStudent")]

        public IActionResult DeleteStudent(string id)
        {
            if (!Guid.TryParse(id, out Guid StudentId))
            {
                return BadRequest(new { message = "Invalid Student ID format." });
            }

            TblStudent? st = stc.TblStudents.Find(StudentId);
            if (st == null)
            {
                return NotFound(new { message = "Student not found." });
            }

            try
            {
                stc.TblStudents.Remove(st);
                stc.SaveChanges();
                return Ok(new { message = "Student deleted successfully.", deletedClass = st });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the cohort id.", error = ex.Message });
            }

        }
    }
}
*/