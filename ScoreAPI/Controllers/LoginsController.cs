using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ScoreAPI.ModelScore2;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ScoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        ScoreContext stc;
        IConfiguration cfg;
        public LoginsController(ScoreContext stc, IConfiguration cfg)
        {
            this.stc = stc;
            this.cfg = cfg;
        }
        [HttpPost]
        [Route("LoginProcess")]
        public IActionResult LoginProcess(string email, string password)
        {
            var user = (object)stc.TblAdmins.FirstOrDefault(x => x.Email == email && x.Password == password) ??
                       (object)stc.TblTeachers.FirstOrDefault(x => x.Email == email && x.Password == password) ??
                       (object)stc.TblStudents.FirstOrDefault(x => x.Email == email && x.Password == password);

            if (user == null)
            {
                return Unauthorized();
            }

            string role = user is TblAdmin ? "Admin" :
                          user is TblTeacher ? "Teacher" :
                          "Student";

            var jwtHandle = new JwtSecurityTokenHandler();
            var key = cfg["jwtSetting:key"];
            var keyByte = Encoding.UTF8.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("tokenid", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyByte), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtHandle.CreateToken(tokenDescriptor);

            return Ok(new { Role = role, Token = jwtHandle.WriteToken(token) });
        }

        // DTO for login request
        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
