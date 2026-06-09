using StudentPanel.Api.Models;
namespace StudentPanel.Api.Data;

public class InMemoryStore
{
    private int _nextStudentId = 4;

    public List<StudentDto> Students { get; } = new()
    {
        new()
        {
            Id = 1, IndexNumber = "s12345", FirstName = "John", LastName = "Doe", Email = "john@example.com",
            Semester = 3
        },
        new()
        {
            Id = 2, IndexNumber = "s12346", FirstName = "Piotr", LastName = "Nowak", Email = "piotr@example.com",
            Semester = 5
        },
        new()
        {
            Id = 3, IndexNumber = "s12347", FirstName = "Maria", LastName = "Bielova", Email = "maria@example.com",
            Semester = 2
        }
    };

    public List<CourseDto> Courses { get; } = new()
    {
        new() { Id = 1, Name = "Math", Ects = 6 },
        new() { Id = 2, Name = "Programming", Ects = 5 },
        new() { Id = 3, Name = "Databases", Ects = 4 },
        new() { Id = 4, Name = "Algorithms", Ects = 3 }
    };

    public List<StudentCourseDto> StudentCourse { get; } = new();

    public StudentDto AddStudent(StudentDto dto)
    {
        dto.Id = _nextStudentId++;
        Students.Add(dto);
        return dto;
    }
}