using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;

namespace WebAPI.Context
{
    public class StudentDBContext:IdentityDbContext<Users>
    {
        public StudentDBContext(DbContextOptions<StudentDBContext> options):base(options)   
        {

        }

        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
