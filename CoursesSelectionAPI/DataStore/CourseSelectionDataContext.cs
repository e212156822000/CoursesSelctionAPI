
using CourseSelectionAPI.Models;
using CoursesSelectionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesSelectionAPI.DataStore
{
    public class CourseSelectionDataContext : BaseDataStoreContext
    {
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Lecturer> Lecturers { get; set; } = null!;
        public DbSet<Lecturer> Students { get; set; } = null!;

        public CourseSelectionDataContext(IDataStoreConfigurator configurator) : base(configurator)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Lecturer>().ToTable("Lecturer");
            modelBuilder.Entity<Student>().ToTable("Student");

        }
    }
}
