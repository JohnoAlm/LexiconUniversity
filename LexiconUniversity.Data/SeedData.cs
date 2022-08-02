using Bogus;
using LexiconUniversity.Core;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LexiconUniversity.Data
{
    public class SeedData
    {
        private static Faker faker = null!;
        public static async Task InitAsync(LexiconUniversityContext db)
        {
            if (await db.Student.AnyAsync()) return;

            faker = new Faker("sv");

            var students = GenerateStudents(50);
            await db.AddRangeAsync(students);

            var courses = GenerateCourses(20);
            await db.AddRangeAsync(courses);

            var enrollments = GenerateEnrollments(courses, students);
            await db.AddRangeAsync(enrollments);

            await db.SaveChangesAsync();
        }

        private static IEnumerable<Enrollment> GenerateEnrollments(IEnumerable<Course> courses, IEnumerable<Student> students)
        {
            Random rnd = new Random();

            var enrollments = new List<Enrollment>();

            foreach (var student in students)
            {
                foreach (var course in courses)
                {
                    if (rnd.Next(0, 5) == 0)
                    {
                        var enrollment = new Enrollment
                        {
                            Course = course,
                            Student = student,
                            Grade = rnd.Next(1, 6)
                        };

                        enrollments.Add(enrollment);
                    }


                }
            }

            return enrollments;

        }

        private static IEnumerable<Course> GenerateCourses(int numberOfCourses)
        {
            var courses = new List<Course>();

            for (int i = 0; i < numberOfCourses; i++)
            {
                // hej jag heter david => Hej Jag Heter David
                var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Company.Bs());

                courses.Add(new Course(title));
            }

            return courses;
        }

        private static IEnumerable<Student> GenerateStudents(int numberOfStudents)
        {
            var students = new List<Student>();

            for (int i = 0; i < numberOfStudents; i++)
            {
                var fName = faker.Name.FirstName();
                var lName = faker.Name.LastName();
                var avatar = faker.Internet.Avatar();
                var email = faker.Internet.Email($"{fName}{lName}");

                var student = new Student(avatar, fName, lName, email)
                {
                    Address = new Address
                    {
                        City = faker.Address.City(),
                        Street = faker.Address.StreetAddress(),
                        ZipCode = faker.Address.ZipCode()
                    }
                };

                students.Add(student);
            }

            return students;
        }
    }
}
