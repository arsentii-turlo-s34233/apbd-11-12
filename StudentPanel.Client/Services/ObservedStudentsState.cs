using StudentPanel.Client.Models;
namespace StudentPanel.Client.Services;

public class ObservedStudentsState
{
    private readonly List<StudentDto> _observed = new();

    public event Action? OnChange;
    public IReadOnlyList<StudentDto> Observed => _observed.AsReadOnly();
    public int Count => _observed.Count;
    public bool IsObserved (int studentId) => _observed.Any( s => s.Id == studentId);

    public void Toggle(StudentDto student)
    {
        if (IsObserved(student.Id))
            _observed.RemoveAll(s => s.Id == student.Id);
        else
            _observed.Add(student);
        OnChange?.Invoke();
    }

    public void Remove(StudentDto student)
    {
        _observed.RemoveAll(s => s.Id == student.Id);
        OnChange?.Invoke();
    }
}