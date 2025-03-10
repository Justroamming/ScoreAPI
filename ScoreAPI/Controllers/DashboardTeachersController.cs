using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreAPI.ModelScore2;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DashboardTeachersController : ControllerBase
    {
        ScoreContext stc;
        public DashboardTeachersController(ScoreContext stc_in)
        {
            stc = stc_in;
        }
        [HttpGet]
        [Route("/DashboardTeachers/GetTeacherById")]
        public IActionResult GetTeacherById(string id)
        {
            return Ok(new { data = stc.TblTeachers.Find(new Guid(id)) });
        }

        [HttpGet]
        [Route("/DashboardTeachers/GetTeacherStudentsInfo")]
        public async Task<IActionResult> GetTeacherStudentsInfo(string id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetTeacherStudentsInfo @TeacherID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@TeacherID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.String;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var studentslist = new List<object>();
                            while (await reader.ReadAsync())
                            {
                                studentslist.Add(new
                                {
                                    teacherName = reader["teacherName"].ToString(),
                                    studentName = reader["studentName"].ToString(),
                                    CohortName = reader["CohortName"].ToString(),
                                    SubjecName = reader["subjectName"].ToString(),
                                });

                            }
                            return Ok(studentslist);
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
        [Route("/DashboardTeachers/GetTeacherGradesStudentsScore")]
        public async Task<IActionResult> GetTeacherGradesStudentsScore(string id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetTeacherGradesStudentsScore @TeacherID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@TeacherID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.String;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var studentsgradelist = new List<object>();
                            while (await reader.ReadAsync())
                            {
                                studentsgradelist.Add(new
                                {
                                    teacherName = reader["teacherName"].ToString(),
                                    studentName = reader["studentName"].ToString(),
                                    CohortName = reader["CohortName"].ToString(),
                                    Score = reader["Score"].ToString(),
                                    GradeDate = reader["GradeDate"].ToString(),
                                    TestType = reader["TestType"].ToString(),
                                    TestDate= reader["TestDate"].ToString(),

                                    SubjecName = reader["subjectName"].ToString(),
                                });

                            }
                            return Ok(studentsgradelist);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
