using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreAPI.ModelScore2;

namespace ScoreAPI.Controllers
{
   // [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class RealAdminsController : ControllerBase
    {
        ScoreContext stc;
        public RealAdminsController(ScoreContext stc_in)
        {
            stc = stc_in;
        }

        [HttpGet]
        [Route("/RealAdmins/GetAllSubjects")]
        public IActionResult GetAllSubjects()
        {
            return Ok(new { data = stc.TblSubjects.ToList() });
        }

        [HttpGet]
        [Route("/RealAdmins/GetSubjectById")]
        public IActionResult GetSubjectById(string id)
        {
            return Ok(new { data = stc.TblSubjects.Find(new Guid(id)) });
        }

        [HttpPost]
        [Route("/RealAdmins/InsertASubject")]
        public IActionResult InsertASubject(string SName)
        {
            TblSubject sub = new TblSubject();
            sub.SubjectId = System.Guid.NewGuid();
            sub.SubjectName = SName;
            stc.TblSubjects.Add(sub);
            stc.SaveChanges();
            return Ok(new { sub });
        }

        [HttpPut]
        [Route("/RealAdmins/UpdateASubject")]
        public IActionResult UpdateASubject(string id, string SName)
        {
            TblSubject sub = new TblSubject();
            sub.SubjectId = new Guid(id);
            sub.SubjectName = SName;
            stc.TblSubjects.Update(sub);
            stc.SaveChanges();
            return Ok(new { sub });
        }

        [HttpDelete]
        [Route("/RealAdmins/DeleteASubject")]
        public IActionResult DeleteSubject(string id)
        {
            TblSubject sub = new TblSubject();
            sub.SubjectId = new Guid(id);
            stc.TblSubjects.Remove(sub);
            stc.SaveChanges();
            return Ok(new { sub });
        }


        [HttpGet]
        [Route("/RealAdmins/GetAllCohorts")]
        public IActionResult GetAllCohorts()
        {
            return Ok(new { data = stc.TblCohorts.ToList() });
        }


        [HttpGet]
        [Route("/RealAdmins/GetNumOfStudentsInACohort")]

        public async Task<IActionResult> GetNumOfStudentsInACohort(Guid id)
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {

                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetStudentCountByCohortID @CohortID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@CohortID";
                        param.Value = id;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var numstudents = new List<object>();

                            while (await reader.ReadAsync())
                            {
                                numstudents.Add(new
                                {
                               
                                    NumOfStudents = Convert.ToInt32(reader["StudentNumbers"]),
                                    CohortName = reader["CohortName"].ToString()

                                });
                            }

                            return Ok(numstudents);
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
        [Route("/RealAdmins/GetCohortById")]
        public IActionResult GetCohortById(string id)
        {
            return Ok(new { data = stc.TblCohorts.Find(new Guid(id)) });
        }


        [HttpPost]
        [Route("/RealAdmins/InsertCohort")]
        public IActionResult InsertCohort(string CName, string Description)
        {
            TblCohort co = new TblCohort();
            co.CohortId = System.Guid.NewGuid();
            co.CohortName = CName;
            co.Description = Description;

            stc.TblCohorts.Add(co);
            stc.SaveChanges();
            return Ok(new { co });

        }

        [HttpPut]
        [Route("/RealAdmins/UpdateCohort")]
        public IActionResult UpdateCohort(string id, string CName, string Description)
        {
            TblCohort co = new TblCohort();
            co.CohortId = new Guid(id);
            co.CohortName = CName;
            co.Description = Description;

            stc.TblCohorts.Update(co);
            stc.SaveChanges();
            return Ok(new { co });
        }

        [HttpDelete]
        [Route("/RealAdmins/DeleteCohort")]
        public IActionResult DeleteCohort(string id)
        {
            TblCohort co = new TblCohort();
            co.CohortId = new Guid(id);

            stc.TblCohorts.Remove(co);
            stc.SaveChanges();

            return Ok(new { co });
        }

        [HttpGet]
        [Route("/RealAdmins/GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            return Ok(new { data = stc.TblStudents.ToList() });
        }


        [HttpGet]
        [Route("/RealAdmins/GetStudentById")]
        public IActionResult GetStudentById(string id)
        {
            return Ok(new { data = stc.TblStudents.Find(new Guid(id)) });
        }


        [HttpPost]
        [Route("/RealAdmins/InsertStudent")]

        public IActionResult InsertStudent(string FName, string LName, string email, string gender,string phone ,string address,DateOnly dob,string password, string cohortID)
        {
            if (!Guid.TryParse(cohortID, out Guid parsedCohortId))
            {
                return BadRequest(new { message = "Invalid GUID format for TeacherID." });
            }

            bool cohortExists = stc.TblCohorts.Any(co => co.CohortId == parsedCohortId);
            if (!cohortExists)
            {
                return NotFound(new { message = "Cohort not found." });
            }

            TblStudent st = new TblStudent
            {
                StudentId = Guid.NewGuid(),
                FirstName = FName,
                LastName = LName,
                Email = email,
                Gender = gender,
                PhoneNumber = phone,
                Address = address,
                DateOfBirth = dob,
                Password = password,
                CohortId = parsedCohortId


            };
            try
            {
                stc.TblStudents.Add(st);
                stc.SaveChanges();
                return Ok(new { st });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";
                return StatusCode(500, new
                {
                    message = "An error occurred while inserting the student.",
                    error = ex.Message,
                    innerError = innerException
                });
            }
        }


        [HttpPut]
        [Route("/RealAdmins/UpdateStudent")]

        public IActionResult UpdateStudent(string id, string FName, string LName, string email, string gender, string phone, string address, DateOnly dob, string password, string cohortID)
        {
            if (!Guid.TryParse(id, out Guid parsedStudentId))
            {
                return BadRequest(new { message = "Invalid Student ID format." });
            }

            if (!Guid.TryParse(cohortID, out Guid parsedCohortId))
            {
                return BadRequest(new { message = "Invalid GUID format for TeacherID." });
            }


            TblStudent? st = stc.TblStudents.Find(parsedStudentId);
            if ((st == null))
            {
                return NotFound(new { message = "Student not found." });
            }

            bool cohortExists = stc.TblCohorts.Any(co => co.CohortId == parsedCohortId);
            if (!cohortExists)
            {
                return NotFound(new { message = "Cohort not found." });
            }

            st.FirstName = FName;
            st.LastName = LName;
            st.Email = email;
            st.Gender = gender;
            st.PhoneNumber = phone;
            st.Address = address;
            st.DateOfBirth = dob;
            st.Password = password;
            st.CohortId = parsedCohortId;

            try
            {
                stc.TblStudents.Update(st);
                stc.SaveChanges();
                return Ok(new { st });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while inserting the student.", error = ex.Message });
            }

        }

        [HttpDelete]
        [Route("/RealAdmins/DeleteStudent")]

        public IActionResult DeleteStudent(string id)
        {
            if (!Guid.TryParse(id, out Guid StudentId))
            {
                return BadRequest(new { message = "Invalid Student ID format." });
            }

            TblStudent? st = stc.TblStudents.Find(StudentId);
            if (st == null)
            {
                return NotFound(new { message = "Student not found." });
            }

            try
            {
                stc.TblStudents.Remove(st);
                stc.SaveChanges();
                return Ok(new { message = "Student deleted successfully.", deletedClass = st });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the cohort id.", error = ex.Message });
            }

        }

        [HttpGet]
        [Route("/RealAdmins/GetAllTeacher")]
        public IActionResult GetAllTeacher()
        {
            return Ok(new { data = stc.TblTeachers.ToList() });
        }

        [HttpGet]
        [Route("/RealAdmins/GetTeacherById")]
        public IActionResult GetTeacherById(string id)
        {
            return Ok(new { data = stc.TblTeachers.Find(new Guid(id)) });
        }

        [HttpPost]
        [Route("/RealAdmins/InsertTeacher")]

        public IActionResult InsertTeacher(string FName, string LName, string email, string gender, string phone, string address, DateOnly dob, string password)
        {
            TblTeacher te = new TblTeacher();
            te.TeacherId = System.Guid.NewGuid();
            te.FirstName = FName;
            te.LastName = LName;
            te.Email = email;
            te.Gender = gender;
            te.PhoneNumber = phone;
            te.Address = address;
            te.DateOfBirth = dob;
            te.Password = password;

            stc.TblTeachers.Add(te);
            stc.SaveChanges();
            return Ok(new { te });

        }

        [HttpPut]
        [Route("/RealAdmins/UpdateTeacher")]

        public IActionResult UpdateTeacher(string id, string FName, string LName, string email, string gender, string phone, string address, DateOnly dob, string password)
        {
            TblTeacher te = new TblTeacher();
            te.TeacherId = new Guid(id);
            te.FirstName = FName;
            te.LastName = LName;
            te.Email = email;
            te.Gender = gender;
            te.PhoneNumber = phone;
            te.Address = address;
            te.DateOfBirth = dob;
            te.Password = password;
            stc.TblTeachers.Update(te);
            stc.SaveChanges();
            return Ok(new { te });

        }

        [HttpDelete]
        [Route("/RealAdmins/DeleteTeacher")]

        public IActionResult DeleteTeacher(string id)
        {
            TblTeacher te = new TblTeacher();
            te.TeacherId = new Guid(id);
            stc.TblTeachers.Remove(te);
            stc.SaveChanges();
            return Ok(new { te });
                
        }

        [HttpGet]
        [Route("/RealAdmins/GetAllTeacherSchedule")]
        public async Task<IActionResult> GetAllTeacherSchedule()
        {
            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetAllTeacherSchedule ";

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var schedules = new List<object>();

                            while (await reader.ReadAsync())
                            {
                                schedules.Add(new
                                {
                                    LessonClassId=reader["LessonClassId"].ToString(),
                                    TeacherId = reader["TeacherId"].ToString(),
                                    CohortId = reader["CohortId"].ToString(),
                                    LessonDate = Convert.ToDateTime(reader["LessonDate"]),
                                    Location = reader["Location"].ToString(),
                                    DayOfWeek= reader["DayOfWeek"].ToString(),
                                    StartTime = reader["StartTime"].ToString(),
                                    EndTime = reader["EndTime"].ToString(),
                                    SubjectId = reader["SubjectId"].ToString(),

                                });
                            }

                            return Ok(schedules);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching student grades.", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("/RealAdmins/GetLessonSchedulebyID")]
        public async Task<IActionResult> GetLessonSchedulebyID(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid lessonClassId))
                {
                    return BadRequest(new { message = "Invalid GUID format." });
                }

                using (var connection = stc.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC GetLessonsSchedulebyID @LessonClassID";
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@LessonClassID";
                        param.Value = lessonClassId;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            var schedule = new List<object>();
                            while (await reader.ReadAsync())
                            {
                                schedule.Add(new
                                {
                                    LessonClassId = reader["LessonClassId"].ToString(),
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


        [HttpPost]
        [Route("/RealAdmins/AssignTeacher")]
        public async Task<IActionResult> AssignTeacher(string subjectID, string teacherID, DateTime lessondate, string location, TimeOnly starttime, TimeOnly endtime, string cohortID)
        {
            if (!Guid.TryParse(subjectID, out Guid parsedSubjectId))
            {
                return BadRequest(new { message = "Invalid GUID format for SubjectID." });
            }
            if (!Guid.TryParse(teacherID, out Guid parsedTeacherId))
            {
                return BadRequest(new { message = "Invalid GUID format for TeacherID." });
            }
            if (!Guid.TryParse(cohortID, out Guid parsedCohortId))
            {
                return BadRequest(new { message = "Invalid GUID format for CohortID." });
            }
            bool subjectExists = stc.TblSubjects.Any(sub => sub.SubjectId == parsedSubjectId);
            if (!subjectExists)
            {
                return NotFound(new { message = "Subject not found." });
            }
            bool teacherExists = stc.TblTeachers.Any(te => te.TeacherId == parsedTeacherId);
            if (!teacherExists)
            {
                return NotFound(new { message = "Teacher not found." });
            }
            bool cohortExists = stc.TblCohorts.Any(co => co.CohortId == parsedCohortId);
            if (!cohortExists)
            {
                return NotFound(new { message = "Cohort not found." });
            }

            try
            {
                using (var connection=stc.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC ASSIGNTEACHER @SubjectID, @TeacherID, @LessonDate, @Location, @StartTime, @EndTime, @CohortID";
                        var param1 = cmd.CreateParameter();
                        param1.ParameterName = "@SubjectID";
                        param1.Value = parsedSubjectId;
                        param1.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param1);
                        var param2 = cmd.CreateParameter();
                        param2.ParameterName = "@TeacherID";
                        param2.Value = parsedTeacherId;
                        param2.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param2);
                        var param3 = cmd.CreateParameter();
                        param3.ParameterName = "@LessonDate";
                        param3.Value = lessondate;
                        param3.DbType = System.Data.DbType.DateTime;
                        cmd.Parameters.Add(param3);
                        var param4 = cmd.CreateParameter();
                        param4.ParameterName = "@Location";
                        param4.Value = location;
                        param4.DbType = System.Data.DbType.String;
                        cmd.Parameters.Add(param4);
                        var param5 = cmd.CreateParameter();
                        param5.ParameterName = "@StartTime";
                        param5.Value = starttime;
                        param5.DbType = System.Data.DbType.Time;
                        cmd.Parameters.Add(param5);
                        var param6 = cmd.CreateParameter();
                        param6.ParameterName = "@EndTime";
                        param6.Value = endtime;
                        param6.DbType = System.Data.DbType.Time;
                        cmd.Parameters.Add(param6);
                        var param7 = cmd.CreateParameter();
                        param7.ParameterName = "@CohortID";
                        param7.Value = parsedCohortId;
                        param7.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param7);
                        await cmd.ExecuteNonQueryAsync();
                        return Ok(new { message = "Teacher assigned successfully." });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("/RealAdmins/UpdateAssignedTeacher")]
        public async Task<IActionResult> UpdateAssignedTeacher(
            string lessonClassID,
            string subjectID,
            string teacherID,
            DateTime lessonDate,
            string location,
            TimeOnly startTime,
            TimeOnly endTime,
            string cohortID)
        {
            // Validate GUIDs
            if (!Guid.TryParse(lessonClassID, out Guid parsedLessonClassId))
            {
                return BadRequest(new { message = "Invalid GUID format for LessonClassID." });
            }
            if (!Guid.TryParse(subjectID, out Guid parsedSubjectId))
            {
                return BadRequest(new { message = "Invalid GUID format for SubjectID." });
            }
            if (!Guid.TryParse(teacherID, out Guid parsedTeacherId))
            {
                return BadRequest(new { message = "Invalid GUID format for TeacherID." });
            }
            if (!Guid.TryParse(cohortID, out Guid parsedCohortId))
            {
                return BadRequest(new { message = "Invalid GUID format for CohortID." });
            }

            try
            {
                using (var connection = stc.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "EXEC UPDATEASSIGNTEACHER @LessonClassID, @SubjectID, @TeacherID, @LessonDate, @Location, @StartTime, @EndTime, @CohortID";

                        // Add parameters
                        var param1 = cmd.CreateParameter();
                        param1.ParameterName = "@LessonClassID";
                        param1.Value = parsedLessonClassId;
                        param1.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param1);

                        var param2 = cmd.CreateParameter();
                        param2.ParameterName = "@SubjectID";
                        param2.Value = parsedSubjectId;
                        param2.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param2);

                        var param3 = cmd.CreateParameter();
                        param3.ParameterName = "@TeacherID";
                        param3.Value = parsedTeacherId;
                        param3.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param3);

                        var param4 = cmd.CreateParameter();
                        param4.ParameterName = "@LessonDate";
                        param4.Value = lessonDate;
                        param4.DbType = System.Data.DbType.DateTime;
                        cmd.Parameters.Add(param4);

                        var param5 = cmd.CreateParameter();
                        param5.ParameterName = "@Location";
                        param5.Value = location;
                        param5.DbType = System.Data.DbType.String;
                        cmd.Parameters.Add(param5);

                        var param6 = cmd.CreateParameter();
                        param6.ParameterName = "@StartTime";
                        param6.Value = startTime;
                        param6.DbType = System.Data.DbType.Time;
                        cmd.Parameters.Add(param6);

                        var param7 = cmd.CreateParameter();
                        param7.ParameterName = "@EndTime";
                        param7.Value = endTime;
                        param7.DbType = System.Data.DbType.Time;
                        cmd.Parameters.Add(param7);

                        var param8 = cmd.CreateParameter();
                        param8.ParameterName = "@CohortID";
                        param8.Value = parsedCohortId;
                        param8.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param8);

                        // Execute the procedure
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return Ok(new { message = "Teacher assignment updated successfully." });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { message = innerMessage });
            }
        }

        [HttpDelete]
        [Route("/RealAdmins/DeleteAssignedTeacher")]
        public async Task<IActionResult> DeleteAssignedTeacher(string lessonClassID)
        {
            // Validate GUID format for LessonClassID
            if (!Guid.TryParse(lessonClassID, out Guid parsedLessonClassId))
            {
                return BadRequest(new { message = "Invalid GUID format for LessonClassID." });
            }

            try
            {
                // Open the database connection
                using (var connection = stc.Database.GetDbConnection())
                {
                    await connection.OpenAsync();
                    using (var cmd = connection.CreateCommand())
                    {
                        // Set the command to execute the stored procedure
                        cmd.CommandText = "EXEC DELETEASSIGNTEACHER @LessonClassID";

                        // Add the LessonClassID parameter
                        var param = cmd.CreateParameter();
                        param.ParameterName = "@LessonClassID";
                        param.Value = parsedLessonClassId;
                        param.DbType = System.Data.DbType.Guid;
                        cmd.Parameters.Add(param);

                        // Execute the stored procedure
                        await cmd.ExecuteNonQueryAsync();
                        return Ok(new { message = "Assigned teacher deleted successfully." });
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors and return a bad request with the error message
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
