using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreAPI.ModelScore2;

namespace ScoreAPI.Controllers
{
    [Authorize(Roles = "Student")]
    [Route("[controller]")]
    [ApiController]
    public class ScheduleStudentsController : ControllerBase
    {
        ScoreContext stc;
        public ScheduleStudentsController(ScoreContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/ScheduleStudents/GetOneStudentSchedule")]
        public async Task<IActionResult> GetOneStudentSchedule(string id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetStudentSchedule @StudentID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@StudentID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.String;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var schedule = new List<object>();
                            while (await reader.ReadAsync())
                            {
                                schedule.Add(new
                                {
                                    studentName = reader["studentName"].ToString(),
                                    CohortName = reader["CohortName"].ToString(),
                                    LessonDate = Convert.ToDateTime(reader["LessonDate"]),
                                    Location = reader["Location"].ToString(),
                                    DayOfWeek = reader["DayOfWeek"].ToString(),
                                    StartTime = reader["StartTime"].ToString(),
                                    EndTime = reader["EndTime"].ToString(),
                                    subjectName = reader["subjectName"].ToString(),
                                    teacherName = reader["teacherName"].ToString()
                                });

                            }
                            return Ok(schedule);
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
