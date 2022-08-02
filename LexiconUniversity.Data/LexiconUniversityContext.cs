using LexiconUniversity.Core;
using Microsoft.EntityFrameworkCore;

namespace LexiconUniversity.Data
{
    public class LexiconUniversityContext : DbContext
    {
        public LexiconUniversityContext(DbContextOptions<LexiconUniversityContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Course { get; set; }

        public DbSet<Student> Student { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FluentAPI
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity<Enrollment>(
                    e => e.HasOne(e => e.Course).WithMany(c => c.Enrollments),
                    e => e.HasOne(e => e.Student).WithMany(s => s.Enrollments));
        }
    }
}
