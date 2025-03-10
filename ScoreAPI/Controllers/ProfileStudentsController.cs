using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreAPI.ModelScore2;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileStudentsController : ControllerBase
    {
        ScoreContext stc;
        public ProfileStudentsController(ScoreContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/ProfileStudents/GetCohortById")]
        public IActionResult GetCohortById(string id)
        {
            return Ok(new { data = stc.TblCohorts.Find(new Guid(id)) });
        }

        [HttpGet]
        [Route("/ProfileStudents/GetStudentOverallAverageScore")]
        public async Task<IActionResult> GetStudentOverallAverageScore(Guid id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {

                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetStudentOverallAverageScore @StudentID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@StudentID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var gpa = new List<object>();

                            while (await reader.ReadAsync())
                            {
                                gpa.Add(new
                                {
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    OverallAverageScore = Convert.ToDouble(reader["OverallAverageScore"]),


                                });
                            }

                            return Ok(gpa);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("/ProfileStudents/UpdateStudentPassword")]
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
                return StatusCode(500, new { message = "An error occurred while updating the student.", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("/ProfileStudents/GetStudentById")]
        public IActionResult GetStudentById(string id)
        {
            return Ok(new { data = stc.TblStudents.Find(new Guid(id)) });
        }
    }
}
