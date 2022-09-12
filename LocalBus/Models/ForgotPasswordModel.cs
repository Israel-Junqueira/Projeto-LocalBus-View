using System.ComponentModel.DataAnnotations;

namespace LocalBus.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
