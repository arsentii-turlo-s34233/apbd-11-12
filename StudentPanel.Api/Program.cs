using StudentPanel.Api.Data;
using StudentPanel.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<InMemoryStore>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    options.AddPolicy("BlazorClient", policy =>
        policy.WithOrigins("http://localhost:5073").AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("BlazorClient");

app.MapGet("/api/students", (InMemoryStore store) => Results.Ok(store.Students));
app.MapGet("/api/students/{id:int}", (int id, InMemoryStore store) =>
{
    var student = store.Students.FirstOrDefault(s => s.Id == id);
    return student is null ? Results.NotFound() : Results.Ok(student);
});

app.MapPost("/api/students", (StudentDto dto, InMemoryStore store) =>
{
    var created = store.AddStudent(dto);
    return Results.Created($"/api/students/{created.Id}", created);
});

app.MapGet("/api/courses", (InMemoryStore store) => Results.Ok(store.Courses));

app.MapPost("/api/students/{id:int}/courses", (int id, StudentCourseDto dto, InMemoryStore store) =>
{
    dto.StudentId = id;
    dto.AssignedAt = DateTime.UtcNow;
    store.StudentCourse.Add(dto);
    return Results.Created($"/api/students/{id}/courses", dto);
});

app.MapGet("/api/students/{id:int}/courses", (int id, InMemoryStore store) =>
{
    var courses = store.StudentCourse.Where(sc => sc.StudentId == id).Select(sc => new
    {
        sc.AssignedAt,
        Course = store.Courses.FirstOrDefault(c => c.Id == sc.CourseId)
    });
    return Results.Ok(courses);
});

app.Run("http://localhost:5001");

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}