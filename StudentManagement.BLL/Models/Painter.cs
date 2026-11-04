namespace StudentManagement.BLL.Models;

public sealed class Painter : Person
{
    public Painter(string name, DateOnly birthDate) : base(name, birthDate)
    {
    }

    public override string Profession => "Painter";
}
