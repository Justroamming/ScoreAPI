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
            if (!Guid.TryParse(id, out Guid teacherId))
            {
                return BadRequest("Invalid ID format.");
            }

            var teacher = stc.TblTeachers.Find(teacherId);
            if (teacher == null)
            {
                return NotFound("Teacher not found.");
            }

            return Ok(new { data = teacher });
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

        [HttpGet]
        [Route("GetTotalStudentsByTeacher")]
        public async Task<IActionResult> GetTotalStudentsByTeacher(Guid id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {

                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetTotalStudentsByTeacher @TeacherID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@TeacherID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var numstudentlist = new List<object>();

                            while (await reader.ReadAsync())
                            {
                                numstudentlist.Add(new
                                {
                                    teacherID = reader["TeacherID"].ToString(),
                                    TotalStudents = reader["totalstudent"].ToString(),

                                });
                            }

                            return Ok(numstudentlist);
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
        [Route("GetAverageScoreByTeacher")]
        public async Task<IActionResult> GetAverageScoreByTeacher(Guid id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {

                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetAverageScoreByTeacher @TeacherID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@TeacherID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var gpastudentlist = new List<object>();

                            while (await reader.ReadAsync())
                            {
                                gpastudentlist.Add(new
                                {
                                    teacherID = reader["TeacherID"].ToString(),
                                    avgScore = reader["AvgScore"].ToString(),

                                });
                            }

                            return Ok(gpastudentlist);
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
