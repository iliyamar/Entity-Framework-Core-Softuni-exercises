
namespace StudentSystem2.Data
{
    using Microsoft.EntityFrameworkCore;
    using StudentSystem2.Domain;

    public class DbContextStudentSystem : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<License> Licenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=. ;Database=StudentSystem2;Trusted_Connection=True ");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<StudentCourse>()
                 .HasKey(k => new { k.StudentId, k.CourseId });


            builder.Entity<StudentCourse>()
                .HasOne(k => k.Student)
                .WithMany(k => k.Courses)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<StudentCourse>()
                .HasOne(k => k.Course)
                .WithMany(k => k.Students)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
