namespace StudentManagement.BLL.Models;

public abstract class Person
{
    protected Person()
    {
    }

    protected Person(string name, DateOnly birthDate)
    {
        Name = name;
        BirthDate = birthDate;
    }

    public string Name { get; set; } = "";
    public DateOnly BirthDate { get; set; }
    public bool HasDriverLicense { get; private set; }

    public abstract string Profession { get; }

    public int GetAge(DateOnly today)
    {
        int age = today.Year - BirthDate.Year;

        if (today.Month < BirthDate.Month ||
            (today.Month == BirthDate.Month && today.Day < BirthDate.Day))
        {
            age--;
        }

        return age;
    }

    // Для отримання посвідчення потрібно мати 18 років і скласти обидва іспити.
    public bool TryGetDriverLicense(bool theoryPassed, bool practicePassed, DateOnly today)
    {
        if (GetAge(today) < 18 || !theoryPassed || !practicePassed)
        {
            return false;
        }

        HasDriverLicense = true;
        return true;
    }
}
