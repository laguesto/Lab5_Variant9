namespace StudentManagement.BLL.Models;

public sealed class Farmer : Person
{
    public Farmer(string name, DateOnly birthDate) : base(name, birthDate)
    {
    }

    public override string Profession => "Farmer";
}
