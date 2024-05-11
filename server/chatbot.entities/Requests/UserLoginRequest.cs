
using System.ComponentModel.DataAnnotations;

namespace chatbot.entities.Requests
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
