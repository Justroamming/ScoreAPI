/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreAPI.ModelScore;
using System.Security.Cryptography;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        ScoreTableContext stc;
        public TestsController(ScoreTableContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/Test/GetAllTests")]
        public IActionResult GetAllTests()
        {
            return Ok(new { data = stc.TblTests.ToList() });
        }

        [HttpGet]
        [Route("/Test/GetTestById")]
        public IActionResult GetTestById(string id)
        {
            return Ok(new { data = stc.TblTests.Find(new Guid(id)) });
        }

        [HttpGet]
        [Route("/Test/GetFutureTestsOfAStudent")]
        public async Task<IActionResult> GetFutureTestsOfAStudent(Guid id){
            try
            {
                using (var connection = stc.Database.GetDbConnection()) {
                    
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

        [HttpPost]
        [Route("/Test/InsertTest")]
        public IActionResult InsertTest(string subjectID, string testtype, DateOnly testdate)
        {
            if (!Guid.TryParse(subjectID, out Guid parsedSubjectId))
            {
                return BadRequest(new { message = "Invalid GUID format for SubjectID." });
            }
            bool subjectExists = stc.TblSubjects.Any(sub => sub.SubjectId == parsedSubjectId);
            if (!subjectExists)
            {
                return NotFound(new { message = "Subject not found." });
            }

            TblTest te = new TblTest
            {
                TestId = Guid.NewGuid(),
                SubjectId = parsedSubjectId,
                TestType = testtype,
                TestDate = testdate
            };

            try
            {
                stc.TblTests.Add(te);
                stc.SaveChanges();
                return Ok(new { message = "Test added successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("/Test/UpdateTest")]
        public IActionResult UpdateTest(string id, string subjectID, string testtype, DateOnly testdate)
        {
            if (!Guid.TryParse(id, out Guid parsedTestId))
            {
                return BadRequest(new { message = "Invalid GUID format for TestID." });
            }
            if (!Guid.TryParse(subjectID, out Guid parsedSubjectId))
            {
                return BadRequest(new { message = "Invalid GUID format for SubjectID." });
            }

            TblTest? te = stc.TblTests.Find(parsedTestId);
            if (te == null)
            {
                return NotFound(new { message = "Test not found." });
            }

            bool subjectExists = stc.TblSubjects.Any(sub => sub.SubjectId == parsedSubjectId);
            if (!subjectExists)
            {
                return NotFound(new { message = "Subject not found." });
            }

            te.SubjectId = parsedSubjectId;
            te.TestType = testtype;
            te.TestDate = testdate;

            try
            {
                stc.TblTests.Update(te);
                stc.SaveChanges();
                return Ok(new { message = "Test updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }

        [HttpDelete]
        [Route("/Test/DeleteTest")]
        public IActionResult DeleteTest(string id)
        {
            if (!Guid.TryParse(id, out Guid parsedTestId))
            {
                return BadRequest(new { message = "Invalid GUID format for TestID." });
            }
            TblTest? te = stc.TblTests.Find(parsedTestId);
            if (te == null)
            {
                return NotFound(new { message = "Test not found." });
            }
            try
            {
                stc.TblTests.Remove(te);
                stc.SaveChanges();
                return Ok(new { message = "Test deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
*/