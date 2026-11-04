using StudentManagement.BLL.Models;
using StudentManagement.BLL.Repositories;

namespace StudentManagement.BLL.Services;

public sealed class StudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public List<Student> GetSecondYearWinterStudents()
    {
        return _repository.LoadAll()
            .Where(student => student.Course == 2 && IsWinter(student.BirthDate.Month))
            .ToList();
    }

    public int CountSecondYearWinterStudents()
    {
        return GetSecondYearWinterStudents().Count;
    }

    public void AddStudent(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);
        ValidateStudent(student);

        List<Student> students = _repository.LoadAll();
        students.Add(student);
        _repository.SaveAll(students);
    }

    private static bool IsWinter(int month)
    {
        return month == 12 || month == 1 || month == 2;
    }

    private static void ValidateStudent(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Surname))
            throw new ArgumentException("Вкажіть прізвище студента.");

        if (string.IsNullOrWhiteSpace(student.Name))
            throw new ArgumentException("Вкажіть ім’я студента.");

        if (student.Course < 1)
            throw new ArgumentException("Курс повинен бути більшим за нуль.");

        if (string.IsNullOrWhiteSpace(student.StudentId))
            throw new ArgumentException("Вкажіть номер студентського квитка.");

        if (student.BirthDate > DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentException("Дата народження не може бути в майбутньому.");
    }
}
