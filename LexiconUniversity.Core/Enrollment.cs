namespace LexiconUniversity.Core
{
#nullable disable
    public class Enrollment
    {
        public int Id { get; set; }

        // Payload
        public int Grade { get; set; }

        // Foreign Keys
        public int StudentId { get; set; }
        public int CourseId { get; set; }


        // Nav props
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
