using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreAPI.ModelScore2;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScoreStudentsController : ControllerBase
    {
        ScoreContext stc;
        public ScoreStudentsController(ScoreContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/ScoreStudentss/GetAllGradesOfAStudent")]
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
    }
}
