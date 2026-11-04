using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentManagement.BLL.Models;

namespace StudentManagement.Tests;

[TestClass]
public class PersonTests
{
    [TestMethod]
    public void TryGetDriverLicense_Under18_ReturnsFalse()
    {
        var painter = new Painter("Олег", new DateOnly(2010, 1, 1));

        bool result = painter.TryGetDriverLicense(true, true, new DateOnly(2026, 1, 1));

        Assert.IsFalse(result);
        Assert.IsFalse(painter.HasDriverLicense);
    }

    [TestMethod]
    public void TryGetDriverLicense_TheoryNotPassed_ReturnsFalse()
    {
        var farmer = new Farmer("Петро", new DateOnly(2000, 1, 1));

        bool result = farmer.TryGetDriverLicense(false, true, new DateOnly(2026, 1, 1));

        Assert.IsFalse(result);
        Assert.IsFalse(farmer.HasDriverLicense);
    }

    [TestMethod]
    public void TryGetDriverLicense_PracticeNotPassed_ReturnsFalse()
    {
        var farmer = new Farmer("Петро", new DateOnly(2000, 1, 1));

        bool result = farmer.TryGetDriverLicense(true, false, new DateOnly(2026, 1, 1));

        Assert.IsFalse(result);
        Assert.IsFalse(farmer.HasDriverLicense);
    }

    [TestMethod]
    public void TryGetDriverLicense_AllConditionsPassed_ReturnsTrue()
    {
        var painter = new Painter("Олег", new DateOnly(2000, 1, 1));

        bool result = painter.TryGetDriverLicense(true, true, new DateOnly(2026, 1, 1));

        Assert.IsTrue(result);
        Assert.IsTrue(painter.HasDriverLicense);
    }

    [TestMethod]
    public void Profession_ReturnsCorrectName()
    {
        var painter = new Painter("Олег", new DateOnly(2000, 1, 1));
        var farmer = new Farmer("Петро", new DateOnly(2000, 1, 1));

        Assert.AreEqual("Painter", painter.Profession);
        Assert.AreEqual("Farmer", farmer.Profession);
    }
}
