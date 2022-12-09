using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; }
        public string? Address { get; set; }
    }
}
