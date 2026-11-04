using System.Globalization;
using System.Text;
using StudentManagement.BLL.Models;
using StudentManagement.BLL.Services;
using StudentManagement.DAL;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

string filePath = Path.Combine(Environment.CurrentDirectory, "students.json");
var repository = new StudentFileRepository(filePath);
var service = new StudentService(repository);

while (true)
{
    Console.WriteLine("\n--- Облік студентів ---");
    Console.WriteLine("1. Додати студента");
    Console.WriteLine("2. Показати всіх студентів");
    Console.WriteLine("3. Студенти 2-го курсу, народжені взимку");
    Console.WriteLine("4. Отримати водійське посвідчення (Painter / Farmer)");
    Console.WriteLine("5. Плавання");
    Console.WriteLine("0. Вихід");
    Console.Write("Ваш вибір: ");

    string? choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                AddStudent();
                break;
            case "2":
                ShowStudents(repository.LoadAll());
                break;
            case "3":
                ShowWinterStudents();
                break;
            case "4":
                GetDriverLicense();
                break;
            case "5":
                Swim();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Невірний пункт меню.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
}

void AddStudent()
{
    Console.Write("Прізвище: ");
    string surname = Console.ReadLine() ?? "";

    Console.Write("Ім'я: ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Курс: ");
    if (!int.TryParse(Console.ReadLine(), out int course))
    {
        Console.WriteLine("Курс потрібно ввести числом.");
        return;
    }

    Console.Write("Номер студентського квитка: ");
    string studentId = Console.ReadLine() ?? "";

    Console.Write("Дата народження (РРРР ММ ДД): ");
    string dateText = Console.ReadLine() ?? "";
    if (!DateOnly.TryParseExact(dateText, "yyyy MM dd", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out DateOnly birthDate))
    {
        Console.WriteLine("Неправильний формат дати. Приклад: 2005 01 25");
        return;
    }

    service.AddStudent(new Student(surname, name, course, studentId, birthDate));
    Console.WriteLine("Студента додано.");
}

void ShowStudents(List<Student> students)
{
    if (students.Count == 0)
    {
        Console.WriteLine("Список студентів порожній.");
        return;
    }

    DateOnly today = DateOnly.FromDateTime(DateTime.Today);
    foreach (Student student in students)
    {
        Console.WriteLine(
            $"{student.Surname} {student.Name} | Курс: {student.Course} | " +
            $"Квиток: {student.StudentId} | Дата народження: {student.BirthDate:yyyy MM dd} | " +
            $"Вік: {student.GetAge(today)}");
    }
}

void ShowWinterStudents()
{
    List<Student> students = service.GetSecondYearWinterStudents();
    Console.WriteLine($"Кількість студентів: {service.CountSecondYearWinterStudents()}");
    ShowStudents(students);
}

void GetDriverLicense()
{
    Console.WriteLine("1. Painter");
    Console.WriteLine("2. Farmer");
    Console.Write("Оберіть сутність: ");
    string? type = Console.ReadLine();

    Console.Write("Ім'я: ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Дата народження (РРРР ММ ДД): ");
    string dateText = Console.ReadLine() ?? "";
    if (!DateOnly.TryParseExact(dateText, "yyyy MM dd", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out DateOnly birthDate))
    {
        Console.WriteLine("Неправильний формат дати.");
        return;
    }

    Person? person = type switch
    {
        "1" => new Painter(name, birthDate),
        "2" => new Farmer(name, birthDate),
        _ => null
    };

    if (person == null)
    {
        Console.WriteLine("Невірний тип сутності.");
        return;
    }

    Console.Write("Теоретичний іспит складено? (так/ні): ");
    bool theoryPassed = Console.ReadLine()?.Trim().ToLower() == "так";

    Console.Write("Практичний іспит складено? (так/ні): ");
    bool practicePassed = Console.ReadLine()?.Trim().ToLower() == "так";

    bool result = person.TryGetDriverLicense(theoryPassed, practicePassed,
        DateOnly.FromDateTime(DateTime.Today));

    Console.WriteLine(result
        ? $"{person.Profession}: водійське посвідчення отримано."
        : "Посвідчення не отримано. Потрібно мати 18 років і скласти обидва іспити.");
}

void Swim()
{
    List<Student> students = repository.LoadAll();
    if (students.Count == 0)
    {
        Console.WriteLine("Спочатку додайте студента.");
        return;
    }

    ShowStudents(students);
    Console.Write("Введіть номер студентського квитка: ");
    string studentId = Console.ReadLine() ?? "";

    Student? student = students.FirstOrDefault(s => s.StudentId == studentId);
    Console.WriteLine(student == null ? "Студента не знайдено." : student.Swim());
}
