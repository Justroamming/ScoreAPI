using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreAPI.ModelScore2;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScoreTeachersController : ControllerBase
    {
        ScoreContext stc;
        public ScoreTeachersController(ScoreContext _stc)
        {
            stc = _stc;
        }

        [HttpGet]
        [Route("GetAllCohortteachbyateacher")]
        public async Task<IActionResult> GetAllCohortteachbyateacher(Guid id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {

                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetAllCohortteachbyateacher @TeacherID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@TeacherID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var cohortlist = new List<object>();

                            while (await reader.ReadAsync())
                            {
                                cohortlist.Add(new
                                {

                                    CohortID = reader["CohortID"].ToString(),
                                    CohortName = reader["CohortName"].ToString()

                                });
                            }

                            return Ok(cohortlist);
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
        [Route("GetTeacherAllTeachStudentsByCohort")]
        public async Task<IActionResult> GetTeacherAllTeachStudentsByCohort(Guid id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {

                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetTeacherAllTeachStudentsByCohort @TeacherID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@TeacherID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var cohortstudentlist = new List<object>();

                            while (await reader.ReadAsync())
                            {
                                cohortstudentlist.Add(new
                                {
                                    StudentID = reader["StudentID"].ToString(),
                                    StudentName = reader["StudentName"].ToString(),

                                    CohortID = reader["CohortID"].ToString(),
                                    CohortName = reader["CohortName"].ToString()

                                });
                            }

                            return Ok(cohortstudentlist);
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
        [Route("GetTeacherAllStudentGrades")]
        public async Task<IActionResult> GetTeacherAllStudentGrades(Guid id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {

                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetTeacherAllStudentGrades @TeacherID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@TeacherID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var cohortstudentlist = new List<object>();

                            while (await reader.ReadAsync())
                            {
                                cohortstudentlist.Add(new
                                {
                                    
                                    StudentID = reader["StudentID"].ToString(),
                                    StudentName = reader["StudentName"].ToString(),
                                    SubjectName = reader["SubjectName"].ToString(),
                                    GradeID = reader["GradeID"].ToString(),
                                    Score = Convert.ToDouble(reader["Score"]),
                                    GradeDate = Convert.ToDateTime(reader["GradeDate"]),
                                    TestID = reader["TestID"].ToString(),
                                    TestType = reader["TestType"].ToString(),
                                    TestDate = Convert.ToDateTime(reader["TestDate"]),
                                    TestWeightID = reader["TestWeightID"].ToString(),
                                    Weight = Convert.ToDouble(reader["Weight"]),
                                   
                                    CohortName = reader["CohortName"].ToString()

                                });
                            }

                            return Ok(cohortstudentlist);
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
        [Route("InsertTeacherStudentGrade")]
        public IActionResult InsertTeacherStudentGrade(string studentID,decimal score,string teacherID,DateTime gradeDate
            ,string subjectID,string testType,DateOnly testDate,decimal weight)
        {
            try { 
            TblTest test = new TblTest();
            test.TestId = System.Guid.NewGuid();
            test.SubjectId = new Guid(subjectID);
            test.TestType = testType;
            test.TestDate = testDate;
            stc.TblTests.Add(test);

            TblTestWeight testWeight = new TblTestWeight();
            testWeight.TestWeightId = System.Guid.NewGuid();
            testWeight.TestId = test.TestId;
            testWeight.Weight = weight;
            stc.TblTestWeights.Add(testWeight);

            TblGrade grade = new TblGrade();
            grade.GradeId = System.Guid.NewGuid();
            grade.StudentId = new Guid(studentID);
            grade.TestId = test.TestId;
            grade.Score = score;
            grade.TeacherId = new Guid(teacherID);
            grade.GradeDate = gradeDate;
            stc.TblGrades.Add(grade);

            stc.SaveChanges();
            return Ok(new {message="Score insert complete"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UpdateTeacherStudentGrade")]
        public IActionResult UpdateTeacherStudentGrade(string gradeID, string studentID, decimal score, string teacherID, DateTime gradeDate,
               string testID, string subjectID, string testType, DateOnly testDate, string testWeightID, decimal weight)
        {
            try
            {
                // Fetch the existing Grade record
                var grade = stc.TblGrades.FirstOrDefault(g => g.GradeId == new Guid(gradeID));
                if (grade == null)
                {
                    return NotFound(new { message = "Grade not found." });
                }

                // Fetch related Test
                var test = stc.TblTests.FirstOrDefault(t => t.TestId == new Guid(testID));
                if (test == null)
                {
                    return NotFound(new { message = "Test not found." });
                }

                // Fetch related TestWeight
                var testWeight = stc.TblTestWeights.FirstOrDefault(w => w.TestWeightId == new Guid(testWeightID));
                if (testWeight == null)
                {
                    return NotFound(new { message = "Test Weight not found." });
                }

                // Update Grade
                grade.StudentId = new Guid(studentID);
                grade.Score = score;
                grade.TeacherId = new Guid(teacherID);
                grade.GradeDate = gradeDate;

                // Update Test
                test.SubjectId = new Guid(subjectID);
                test.TestType = testType;
                test.TestDate = testDate;

                // Update Test Weight
                testWeight.Weight = weight;

                // Save changes in one transaction
                stc.SaveChanges();
                return Ok(new { message = "Score update complete" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpDelete]
        [Route("DeleteTeacherStudentGrade")]
        public IActionResult DeleteTeacherStudentGrade(string gradeID)
        {
            using var transaction = stc.Database.BeginTransaction();
            try
            {
                // Fetch the existing Grade record
                var grade = stc.TblGrades.FirstOrDefault(g => g.GradeId == new Guid(gradeID));
                if (grade == null)
                {
                    return NotFound(new { message = "Grade not found." });
                }

                // Fetch related Test and TestWeight records
                var test = stc.TblTests.FirstOrDefault(t => t.TestId == grade.TestId);
                if (test == null)
                {
                    return NotFound(new { message = "Test not found." });
                }

                var testWeight = stc.TblTestWeights.FirstOrDefault(w => w.TestId == grade.TestId);
                if (testWeight != null)  // Test weight might not exist, so handle gracefully
                {
                    stc.TblTestWeights.Remove(testWeight);
                }

                // Delete Grade first to avoid foreign key constraints
                stc.TblGrades.Remove(grade);

                // Then delete the Test
                stc.TblTests.Remove(test);

                // Commit the transaction
                stc.SaveChanges();
                transaction.Commit();

                return Ok(new { message = "Score deletion complete" });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("GetStudentsInCohort")]
        public async Task<IActionResult> GetStudentsInCohort(Guid id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {

                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetStudentsInCohort @CohortID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@CohortID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var studentsinfo = new List<object>();

                            while (await reader.ReadAsync())
                            {
                                studentsinfo.Add(new
                                {

                                    StudentFullName = reader["StudentFullName"].ToString(),
                                    StudentGender = reader["Gender"].ToString(),
                                    StudentEmail = reader["Email"].ToString(),
                                    StudentPhone = reader["PhoneNumber"].ToString(),
                                    StudentAddress = reader["Address"].ToString(),
                                    StudentDOB = reader["DateOfBirth"].ToString(),


                                });
                            }

                            return Ok(studentsinfo);
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
        [Route("GetTeacherAllSubjectsTeach")]
        public async Task<IActionResult> GetTeacherAllSubjectsTeach(Guid id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {

                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetTeacherAllSubjectsTeach @TeacherID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@TeacherID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var teachersubjectlist = new List<object>();

                            while (await reader.ReadAsync())
                            {
                                teachersubjectlist.Add(new
                                {
                                    teacherName = reader["TeacherName"].ToString(),
                                    subjectName = reader["SubjectName"].ToString(),
                                    subjectID = reader["SubjectID"].ToString(),
                                });
                            }

                            return Ok(teachersubjectlist);
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
