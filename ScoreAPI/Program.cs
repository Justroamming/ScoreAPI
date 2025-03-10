using Microsoft.EntityFrameworkCore;
using ScoreAPI.ModelScore2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string strcnn = builder.Configuration.GetConnectionString("cnn");
builder.Services.AddDbContext<ScoreContext>(options => options.UseSqlServer(strcnn));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
