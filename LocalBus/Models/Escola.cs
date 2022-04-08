using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalBus.Models
{   [Table("Escola")]
    public class Escola
    {
        [Key]
        public int EscolaId { get; set; }

        [Required(ErrorMessage ="E necessario um nome para escola")]
        [StringLength(100,MinimumLength =10,ErrorMessage ="O nome é inferior a 10 caracteres ou superior a 100 caracteres ")]
        [Display(Name ="Insira o nome da escola")]
        public string NomeEscola { get; set; }

        [Required(ErrorMessage = "E necessario um telefone para escola")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Insira o telefone da escola")]
        public string TelefoneDaEscola { get; set; }

        [Required(ErrorMessage = "E necessario dar um E-mail para escola")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Insira o E-mail da escola")]
        public string EmailDaEscola { get; set; }

        [Required(ErrorMessage = "E necessario informar o Codigo da escola")]
        [Display(Name = "Insira o codigo da escola")]
        public int CodigoDaEtec { get; set; }

        public ICollection<EscolaPonto> Escola_Ponto { get; set; }
        public ICollection<ImagemEscola> ImagemEscolas { get; set; }


    }
}
