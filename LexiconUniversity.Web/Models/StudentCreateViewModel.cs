using LexiconUniversity.Web.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LexiconUniversity.Web.Models
{
#nullable disable
    public class StudentCreateViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [KalleAnka(ErrorMessage = "Måste heta Kalle Anka!")]
        public string LastName { get; set; }

        [EmailAddress]
        [Remote(action: "CheckIfEmailIsUnique", controller: "Students")]
        public string Email { get; set; }
        public string AddressStreet { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressCity { get; set; }
    }
}
