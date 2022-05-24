using System.ComponentModel.DataAnnotations;

namespace LocalBus.ViewModels
{
    public class LoginViewModel
    {
        
            [Required(ErrorMessage = "Informe o nome")]
            [Display(Name = "usuario")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Informe a senha")]
            [Display(Name = "senha")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            public string ReturnUrl { get; set; }
        
    }
}
