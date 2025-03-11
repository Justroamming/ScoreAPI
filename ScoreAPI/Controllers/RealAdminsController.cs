using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreAPI.ModelScore2;

namespace ScoreAPI.Controllers
{
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
                return StatusCode(500, new { message = "An error occurred while inserting the student.", error = ex.Message });
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
    }
}
