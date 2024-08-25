using CoursesSelectionAPI.Init;
using CoursesSelectionAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogging();

// Add services to the container.

builder.Services
    .AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddSingleton<ICourseRepository, FakeCourseRepository>()
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();

public partial class Program { }

