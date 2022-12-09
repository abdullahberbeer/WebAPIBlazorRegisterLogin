using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class StudentManager : IStudenService
    {
        private readonly StudentDBContext _context;

        public StudentManager(StudentDBContext context)
        {
            _context = context;
        }

        public async Task<MainResponse> AddStudent(StudentDTO studentDTO)
        {
            var response = new MainResponse();
            try
            {
                if (_context.Students.Any(f=>f.Email.ToLower()==studentDTO.Email.ToLower()))
                {
                    response.ErrorMessage = "Bu email adresi ile daha önce öğreci eklenmiştir..";
                    response.IsSuccess = false;
                }
                else
                {
                    await _context.AddAsync(new Student
                    {
                        Email=studentDTO.Email,
                        Address=studentDTO.Address,
                        FirstName=studentDTO.FirstName,
                        Gender=studentDTO.Gender,
                        LastName=studentDTO.LastName
                    });

                    await _context.SaveChangesAsync();
                    response.IsSuccess = true;
                    response.Content = "Öğrenci sisteme başarıyla eklendi.. ";
                }
            }
            catch (Exception ex)
            {

                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<MainResponse> DeleteStudent(DeleteStudentDTO deleteStudentDTO)
        {
            var response = new MainResponse();
            try
            {
                if (deleteStudentDTO.StudentID<0)
                {
                    response.ErrorMessage = "Id bulunamadı..";
                    response.IsSuccess = false;
                    return response;
                }
                var existingStudent = _context.Students.Where(f => f.StudentID == deleteStudentDTO.StudentID).FirstOrDefault();
                if (existingStudent!=null)
                {
                    _context.Remove(existingStudent);
                    await _context.SaveChangesAsync();
                    response.IsSuccess = true;
                    response.Content = "Öğrenci başarıyla silindi..";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Content = "Öğrenci bulunamadı..";
                }
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<MainResponse> GetAllStudent()
        {
            var response = new MainResponse();
            try
            {
                response.Content = await _context.Students.ToListAsync();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage=ex.Message;
                response.IsSuccess=false;
            }
            return response;
        }

        public async Task<MainResponse> GetStudentById(int studentId)
        {
            var response = new MainResponse();
            try
            {
                response.Content =
                    await _context.Students.Where(f => f.StudentID == studentId).FirstOrDefaultAsync();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<MainResponse> UpdateStudent(UpdateStudentDTO updateStudentDTO)
        {
            var response = new MainResponse();
            try
            {
                if (updateStudentDTO.StudentID<0)
                {
                    response.ErrorMessage = "Öğrenci Id bulunamadı..";
                    response.IsSuccess = false;
                    return response;

                }
                var existingStudent = _context.Students.Where(f => f.StudentID == updateStudentDTO.StudentID).FirstOrDefault();
                if (existingStudent!=null)
                {
                    existingStudent.FirstName = updateStudentDTO.FirstName;
                    existingStudent.LastName = updateStudentDTO.LastName;
                    existingStudent.Email = updateStudentDTO.Email;
                    existingStudent.Gender = updateStudentDTO.Gender;
                    existingStudent.Address = updateStudentDTO.Address;
                    await _context.SaveChangesAsync();
                    response.IsSuccess = true;
                    response.Content = "Öğrenci başarıyla güncellendi..";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Content = "Sistemde kayıtlı öğrenci bulunamadı..";
                }
            }
            catch (Exception ex)
            {

                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}
