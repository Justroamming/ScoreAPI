/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreAPI.ModelScore;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        ScoreTableContext stc;
        public GradesController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/Grade/GetAllGrades")]
        public IActionResult GetAllGrades()
        {
            return Ok(new { data = stc.TblGrades.ToList() });
        }

        [HttpGet]
        [Route("/Grade/GetStudentAverageScoreBySubject")]
        public async Task<IActionResult> GetStudentAverageScoreBySubject(Guid id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {

                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetStudentAverageScoreBySubject @StudentID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@StudentID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var averagescores = new List<object>();

                            while (await reader.ReadAsync())
                            {
                                averagescores.Add(new
                                {
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    SubjectName = reader["SubjectName"].ToString(),
                                    AverageScore = Convert.ToDouble(reader["AverageScore"]),
                                });
                            }

                            return Ok(averagescores);
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
        [Route("/Grade/GetStudentOverallAverageScore")]
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

        [HttpPost]
        [Route("/Grade/InsertGrade")]
        public IActionResult InsertGrade(string studentID, string testID, decimal score, string teacherID, DateTime gradedate, decimal weight)
        {
            if (!Guid.TryParse(studentID, out Guid parsedStudentId))
            {
                return BadRequest(new { message = "Invalid GUID format for StudentID." });
            }
            if (!Guid.TryParse(testID, out Guid parsedTestId))
            {
                return BadRequest(new { message = "Invalid GUID format for TestID." });
            }
            if (!Guid.TryParse(teacherID, out Guid parsedTeacherId))
            {
                return BadRequest(new { message = "Invalid GUID format for TeacherID." });
            }

            bool studentExists = stc.TblStudents.Any(st => st.StudentId == parsedStudentId);
            if (!studentExists)
            {
                return NotFound(new { message = "Student not found." });
            }

            bool testExists = stc.TblTests.Any(te => te.TestId == parsedTestId);
            if (!testExists)
            {
                return NotFound(new { message = "Test not found." });
            }

            bool teacherExists = stc.TblTeachers.Any(te => te.TeacherId == parsedTeacherId);
            if (!teacherExists)
            {
                return NotFound(new { message = "Teacher not found." });
            }

            TblGrade gr = new TblGrade
            {
                GradeId = Guid.NewGuid(),
                StudentId = parsedStudentId,
                TestId = parsedTestId,
                Score = score,
                TeacherId = parsedTeacherId,
                GradeDate = gradedate,
                Weight = weight
            };

            try
            {
                stc.TblGrades.Add(gr);
                stc.SaveChanges();
                return Ok(new { message = "Grade inserted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut]
        [Route("/Grade/UpdateGrade")]
        public IActionResult UpdateGrade(string id, string studentID, string testID, decimal score, string teacherID, DateTime gradedate, decimal weight)
        {
            if (!Guid.TryParse(id, out Guid parsedGradeId))
            {
                return BadRequest(new { message = "Invalid GUID format for GradeID." });
            }
            if (!Guid.TryParse(studentID, out Guid parsedStudentId))
            {
                return BadRequest(new { message = "Invalid GUID format for StudentID." });
            }
            if (!Guid.TryParse(testID, out Guid parsedTestId))
            {
                return BadRequest(new { message = "Invalid GUID format for TestID." });
            }
            if (!Guid.TryParse(teacherID, out Guid parsedTeacherId))
            {
                return BadRequest(new { message = "Invalid GUID format for TeacherID." });
            }

            TblGrade? gr = stc.TblGrades.Find(parsedGradeId);
            if (gr == null)
            {
                return NotFound(new { message = "Grade not found." });
            }

            bool studentExists = stc.TblStudents.Any(st => st.StudentId == parsedStudentId);
            if (!studentExists)
            {
                return NotFound(new { message = "Student not found." });
            }
            bool testExists = stc.TblTests.Any(te => te.TestId == parsedTestId);
            if (!testExists)
            {
                return NotFound(new { message = "Test not found." });
            }
            bool teacherExists = stc.TblTeachers.Any(te => te.TeacherId == parsedTeacherId);
            if (!teacherExists)
            {
                return NotFound(new { message = "Teacher not found." });
            }

            gr.StudentId = parsedStudentId;
            gr.TestId = parsedTestId;
            gr.Score = score;
            gr.TeacherId = parsedTeacherId;
            gr.GradeDate = gradedate;
            gr.Weight = weight;

            try
            {
                stc.TblGrades.Update(gr);
                stc.SaveChanges();
                return Ok(new { message = "Grade updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("/Grade/DeleteGrade")]
        public IActionResult DeleteGrade(string id)
        {
            if (!Guid.TryParse(id, out Guid parsedGradeId))
            {
                return BadRequest(new { message = "Invalid GUID format for GradeID." });
            }
            TblGrade? gr = stc.TblGrades.Find(parsedGradeId);
            if (gr == null)
            {
                return NotFound(new { message = "Grade not found." });
            }
            try
            {
                stc.TblGrades.Remove(gr);
                stc.SaveChanges();
                return Ok(new { message = "Grade deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }
    }
}
*/