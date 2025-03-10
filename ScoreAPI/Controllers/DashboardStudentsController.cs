using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreAPI.ModelScore2;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DashboardStudentsController : ControllerBase
    {
        ScoreContext stc;
        public DashboardStudentsController(ScoreContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/DashboardStudents/GetStudentById")]
        public IActionResult GetStudentById(string id)
        {
            return Ok(new { data = stc.TblStudents.Find(new Guid(id)) });
        }

        [HttpGet]
        [Route("/DashboardStudents/GetStudentOverallAverageScore")]
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

        [HttpGet]
        [Route("/DashboardStudents/GetFutureTestsOfAStudent")]
        public async Task<IActionResult> GetFutureTestsOfAStudent(Guid id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {

                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetFutureTestsForStudent @StudentID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@StudentID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var futuretests = new List<object>();

                            while (await reader.ReadAsync())
                            {
                                futuretests.Add(new
                                {
                                    SubjectName = reader["SubjectName"].ToString(),
                                    TestType = reader["TestType"].ToString(),
                                    TestDate = Convert.ToDateTime(reader["TestDate"]),


                                });
                            }

                            return Ok(futuretests);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet]
        [Route("/DashboardStudents/GetAllGradesOfAStudent")]
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
