using System.ComponentModel.DataAnnotations;
namespace LocalBus.Models
{
    public class Administrador
    {
        public int AdministradorId { get; set; }

        [Required(ErrorMessage = "E necessario um nome")]
        [Display(Name = "Insira o nome do admin")]
        [StringLength(50)]
        public string NomeAdministrador { get; set; }

        [Required(ErrorMessage = "E necessario um Telefone")]
        [Display(Name = "Insira o telefone do admin")]
        [DataType(DataType.PhoneNumber)]
        public string TelefoneAdmnistrador { get; set; }

        [Required(ErrorMessage = "E necessario um E-mail")]
        [Display(Name = "Insira o E-mail do admin")]
        [DataType(DataType.EmailAddress)]
        public string EmailAdministrador { get; set; }


        [Required(ErrorMessage = "E necessario um Cpf")]
        [Display(Name = "Insira o CPF do admin")]
        public string CPFAdministrador { get; set; }

        [Display(Name = "Insira o login")]
        [Required(ErrorMessage = "E necessario inserir uma senha")]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="E necessario inserir uma senha")]
        [Display(Name = "Insira a senha")]
        public string Senha { get; set; }

        public int EscolaId { get; set; }
        public virtual Escola Escola { get; set; }
    }
}
