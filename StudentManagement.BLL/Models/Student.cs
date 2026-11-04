namespace StudentManagement.BLL.Models;

public sealed class Student : Person
{
    public Student()
    {
    }

    public Student(string surname, string name, int course, string studentId, DateOnly birthDate)
        : base(name, birthDate)
    {
        Surname = surname;
        Course = course;
        StudentId = studentId;
    }

    public string Surname { get; set; } = "";
    public int Course { get; set; }
    public string StudentId { get; set; } = "";

    public override string Profession => "Студент";

    public string Swim()
    {
        return $"{Name} {Surname} вміє плавати.";
    }
}
