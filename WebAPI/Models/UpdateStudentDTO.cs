namespace WebAPI.Models
{
    public class UpdateStudentDTO
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } 
        public string Gender { get; set; } 
        public string? Address { get; set; }
    }
}
