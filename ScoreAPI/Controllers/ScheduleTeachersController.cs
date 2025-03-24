using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreAPI.ModelScore2;

namespace ScoreAPI.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Route("[controller]")]
    [ApiController]
    public class ScheduleTeachersController : ControllerBase
    {
        ScoreContext stc;
        public ScheduleTeachersController(ScoreContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/ScheduleTeachers/GetOneTeacherSchedule")]
        public async Task<IActionResult> GetOneTeacherSchedule(string id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetTeacherLessonsSchedule @TeacherID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@TeacherID";
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
                                    teacherName = reader["teacherName"].ToString(),
                                    CohortName = reader["CohortName"].ToString(),
                                    LessonDate = Convert.ToDateTime(reader["LessonDate"]),
                                    Location = reader["Location"].ToString(),
                                    DayOfWeek = reader["DayOfWeek"].ToString(),
                                    StartTime = reader["StartTime"].ToString(),
                                    EndTime = reader["EndTime"].ToString(),
                                    subjectName = reader["subjectName"].ToString()
                                    
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
