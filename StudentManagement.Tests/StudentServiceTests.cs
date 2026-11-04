using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentManagement.BLL.Models;
using StudentManagement.BLL.Repositories;
using StudentManagement.BLL.Services;

namespace StudentManagement.Tests;

[TestClass]
public class StudentServiceTests
{
    private MemoryStudentRepository _repository = null!;
    private StudentService _service = null!;

    [TestInitialize]
    public void Setup()
    {
        _repository = new MemoryStudentRepository();
        _service = new StudentService(_repository);
    }

    [TestMethod]
    public void Constructor_NullRepository_ThrowsException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new StudentService(null!));
    }

    [TestMethod]
    public void CountSecondYearWinterStudents_ReturnsOnlyMatchingStudents()
    {
        _repository.Students.Add(new Student("Зимовий", "Один", 2, "1", new DateOnly(2005, 12, 5)));
        _repository.Students.Add(new Student("Зимовий", "Два", 2, "2", new DateOnly(2005, 1, 5)));
        _repository.Students.Add(new Student("Зимовий", "Три", 2, "3", new DateOnly(2005, 2, 5)));
        _repository.Students.Add(new Student("Літній", "Чотири", 2, "4", new DateOnly(2005, 6, 5)));
        _repository.Students.Add(new Student("Інший", "Курс", 3, "5", new DateOnly(2005, 1, 5)));

        int count = _service.CountSecondYearWinterStudents();

        Assert.AreEqual(3, count);
    }

    [TestMethod]
    public void GetSecondYearWinterStudents_ReturnsMatchingStudentData()
    {
        var expected = new Student("Іваненко", "Іван", 2, "123", new DateOnly(2005, 12, 10));
        _repository.Students.Add(expected);
        _repository.Students.Add(new Student("Петренко", "Петро", 1, "456", new DateOnly(2005, 1, 10)));

        List<Student> result = _service.GetSecondYearWinterStudents();

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("123", result[0].StudentId);
    }

    [TestMethod]
    public void CountSecondYearWinterStudents_EmptyList_ReturnsZero()
    {
        Assert.AreEqual(0, _service.CountSecondYearWinterStudents());
    }

    [TestMethod]
    public void AddStudent_ValidStudent_SavesStudent()
    {
        var student = new Student("Іваненко", "Іван", 2, "123", new DateOnly(2005, 5, 10));

        _service.AddStudent(student);

        Assert.AreEqual(1, _repository.Students.Count);
        Assert.AreEqual("123", _repository.Students[0].StudentId);
    }

    [TestMethod]
    public void AddStudent_Null_ThrowsException()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _service.AddStudent(null!));
    }

    [TestMethod]
    public void AddStudent_EmptySurname_ThrowsException()
    {
        var student = new Student("", "Іван", 2, "123", new DateOnly(2005, 5, 10));
        Assert.ThrowsException<ArgumentException>(() => _service.AddStudent(student));
    }

    [TestMethod]
    public void AddStudent_EmptyName_ThrowsException()
    {
        var student = new Student("Іваненко", "", 2, "123", new DateOnly(2005, 5, 10));
        Assert.ThrowsException<ArgumentException>(() => _service.AddStudent(student));
    }

    [TestMethod]
    public void AddStudent_InvalidCourse_ThrowsException()
    {
        var student = new Student("Іваненко", "Іван", 0, "123", new DateOnly(2005, 5, 10));
        Assert.ThrowsException<ArgumentException>(() => _service.AddStudent(student));
    }

    [TestMethod]
    public void AddStudent_EmptyStudentId_ThrowsException()
    {
        var student = new Student("Іваненко", "Іван", 2, "", new DateOnly(2005, 5, 10));
        Assert.ThrowsException<ArgumentException>(() => _service.AddStudent(student));
    }

    [TestMethod]
    public void AddStudent_FutureBirthDate_ThrowsException()
    {
        var student = new Student("Іваненко", "Іван", 2, "123", DateOnly.MaxValue);
        Assert.ThrowsException<ArgumentException>(() => _service.AddStudent(student));
    }

    private sealed class MemoryStudentRepository : IStudentRepository
    {
        public List<Student> Students { get; } = new();

        public List<Student> LoadAll()
        {
            return new List<Student>(Students);
        }

        public void SaveAll(List<Student> students)
        {
            Students.Clear();
            Students.AddRange(students);
        }
    }
}
