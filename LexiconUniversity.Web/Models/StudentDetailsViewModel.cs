using LexiconUniversity.Core;

namespace LexiconUniversity.Web.Models
{
#nullable disable
    public class StudentDetailsViewModel
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AddressStreet { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressCity { get; set; }
        public int NrOfEnrollments { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
