using System.Net.Http.Json;
using StudentPanel.Client.Models;
namespace StudentPanel.Client.Services;

public class StudentsApiClient(HttpClient http)
{
    public async Task<List<StudentDto>> GetStudentsAsync() =>
        await http.GetFromJsonAsync<List<StudentDto>>("api/students") ?? new();
    public async Task<StudentDto?> GetStudentAsync(int id) =>
        await http.GetFromJsonAsync<StudentDto>($"api/students/{id}");

    public async Task<StudentDto?> CreateStudentAsync(StudentDto dto)
    {
        var response = await http.PostAsJsonAsync("/api/students", dto);
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<StudentDto>() :  null;
    }
    
    public async Task<List<CourseDto>> GetCoursesAsync() =>
        await http.GetFromJsonAsync<List<CourseDto>>("api/courses") ?? new();
    
    public async Task<List<StudentCourseAssignment>> GetStudentCoursesAsync(int studentId) =>
        await http.GetFromJsonAsync<List<StudentCourseAssignment>>($"api/students/{studentId}/courses") ?? new();

    public async Task<bool> AssignCourseAsync(int studentId, int courseId)
    {
        var response = await http.PostAsJsonAsync(
            $"/api/students/{studentId}/courses",
            new { CourseId = courseId });
        return response.IsSuccessStatusCode;
    }
}