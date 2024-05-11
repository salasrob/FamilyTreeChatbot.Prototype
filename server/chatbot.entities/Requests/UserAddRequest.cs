
using System.ComponentModel.DataAnnotations;

namespace chatbot.entities.Requests
{
    public class UserAddRequest
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$",
            ErrorMessage = "Password must be between 8 and 20 characters and contain one uppercase letter, " +
            "one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        public bool IsAccountActive { get; set; }
    }
}
