using System.ComponentModel.DataAnnotations;

namespace LocalBus.Models
{
    public class ResetPasswordModel
    {
        public string Token { get; set; }
        [DataType(DataType.Password)]
        public string Email { get; set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
