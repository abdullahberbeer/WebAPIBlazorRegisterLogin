using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IStudenService
    {
        Task<MainResponse> GetAllStudent();
        Task<MainResponse> AddStudent(StudentDTO studentDTO);
        Task<MainResponse> UpdateStudent(UpdateStudentDTO updateStudentDTO);
        Task<MainResponse> DeleteStudent(DeleteStudentDTO deleteStudentDTO);
        Task<MainResponse> GetStudentById(int studentId);
    }
}
