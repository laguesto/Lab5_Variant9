using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentManagement.BLL.Models;

namespace StudentManagement.Tests;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void GetAge_BeforeBirthday_ReturnsPreviousAge()
    {
        var student = new Student("Іваненко", "Іван", 2, "123", new DateOnly(2005, 5, 10));

        int age = student.GetAge(new DateOnly(2026, 5, 9));

        Assert.AreEqual(20, age);
    }

    [TestMethod]
    public void GetAge_OnBirthday_IncreasesAge()
    {
        var student = new Student("Іваненко", "Іван", 2, "123", new DateOnly(2005, 5, 10));

        int age = student.GetAge(new DateOnly(2026, 5, 10));

        Assert.AreEqual(21, age);
    }

    [TestMethod]
    public void Profession_ReturnsStudent()
    {
        var student = new Student("Іваненко", "Іван", 2, "123", new DateOnly(2005, 5, 10));

        Assert.AreEqual("Студент", student.Profession);
    }

    [TestMethod]
    public void Swim_ReturnsMessage()
    {
        var student = new Student("Іваненко", "Іван", 2, "123", new DateOnly(2005, 5, 10));

        string result = student.Swim();

        Assert.AreEqual("Іван вміє плавати.", result);
    }
}
