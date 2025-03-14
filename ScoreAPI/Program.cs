using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ScoreAPI.ModelScore2;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string strcnn = builder.Configuration.GetConnectionString("cnn");
builder.Services.AddDbContext<ScoreContext>(options => options.UseSqlServer(strcnn));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var key =builder.Configuration["JwtSetting:Key"];
byte[] keyMahoa = Encoding.UTF8.GetBytes(key);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,


        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(keyMahoa),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
    options.AddPolicy("Teacher", policy => policy.RequireClaim("Role", "Teacher"));
    options.AddPolicy("Student", policy => policy.RequireClaim("Role", "Student"));
});

var app = builder.Build();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
