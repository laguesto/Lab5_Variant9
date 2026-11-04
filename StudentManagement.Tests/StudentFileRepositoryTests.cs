using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentManagement.BLL.Models;
using StudentManagement.DAL;

namespace StudentManagement.Tests;

[TestClass]
public class StudentFileRepositoryTests
{
    [TestMethod]
    public void LoadAll_FileDoesNotExist_ReturnsEmptyList()
    {
        string path = CreateTempFilePath();
        var repository = new StudentFileRepository(path);

        List<Student> result = repository.LoadAll();

        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void SaveAll_ThenLoadAll_ReturnsSavedStudents()
    {
        string path = CreateTempFilePath();
        var repository = new StudentFileRepository(path);
        var students = new List<Student>
        {
            new("Іваненко", "Іван", 2, "123", new DateOnly(2005, 1, 15))
        };

        repository.SaveAll(students);
        List<Student> loaded = repository.LoadAll();

        Assert.AreEqual(1, loaded.Count);
        Assert.AreEqual("Іваненко", loaded[0].Surname);
        Assert.AreEqual("123", loaded[0].StudentId);

        File.Delete(path);
    }

    [TestMethod]
    public void LoadAll_EmptyFile_ReturnsEmptyList()
    {
        string path = CreateTempFilePath();
        File.WriteAllText(path, " ");
        var repository = new StudentFileRepository(path);

        List<Student> result = repository.LoadAll();

        Assert.AreEqual(0, result.Count);
        File.Delete(path);
    }

    [TestMethod]
    public void Constructor_EmptyPath_ThrowsException()
    {
        Assert.ThrowsException<ArgumentException>(() => new StudentFileRepository(" "));
    }

    [TestMethod]
    public void SaveAll_Null_ThrowsException()
    {
        string path = CreateTempFilePath();
        var repository = new StudentFileRepository(path);

        Assert.ThrowsException<ArgumentNullException>(() => repository.SaveAll(null!));
    }

    private static string CreateTempFilePath()
    {
        return Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".json");
    }
}
