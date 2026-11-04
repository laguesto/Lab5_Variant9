using StudentManagement.BLL.Models;

namespace StudentManagement.BLL.Repositories;

public interface IStudentRepository
{
    List<Student> LoadAll();
    void SaveAll(List<Student> students);
}
