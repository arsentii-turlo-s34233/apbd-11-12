namespace StudentPanel.Client.Models;

public class StudentCourseAssignment
{
    public int CourseId { get; set; }
    public DateTime AssignedAt { get; set; }
    public CourseDto? Course { get; set; }
}