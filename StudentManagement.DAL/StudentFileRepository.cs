using System.Text.Json;
using StudentManagement.BLL.Models;
using StudentManagement.BLL.Repositories;

namespace StudentManagement.DAL;

public sealed class StudentFileRepository : IStudentRepository
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

    public StudentFileRepository(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("Не вказано шлях до файлу.", nameof(filePath));

        _filePath = filePath;
    }

    public List<Student> LoadAll()
    {
        if (!File.Exists(_filePath))
            return new List<Student>();

        string json = File.ReadAllText(_filePath);
        if (string.IsNullOrWhiteSpace(json))
            return new List<Student>();

        return JsonSerializer.Deserialize<List<Student>>(json) ?? new List<Student>();
    }

    public void SaveAll(List<Student> students)
    {
        ArgumentNullException.ThrowIfNull(students);

        string? directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);

        string json = JsonSerializer.Serialize(students, _options);
        File.WriteAllText(_filePath, json);
    }
}
