using LocalBus.Models;
using System.ComponentModel.DataAnnotations;

namespace LocalBus.ViewModels
{
    public class RegistroViewModel
    {

        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Error, Valide sua senha")]
        [Display(Name = "senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public int CodigoEtec { get; set; }

        [Required(ErrorMessage = "E necessario um E-mail para escola")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Insira o E-mail da escola")]
        public string Email { get; set; }
        [Required(ErrorMessage = "E necessario um telefone para escola")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Insira o telefone da escola")]
        public string TelefonedaEscola { get; set; }
        [Required(ErrorMessage = "Informe o nome da escola")]
        [Display(Name = "Nome da Escola")]
        public string NomedaEscola { get; set; }
    }
}
