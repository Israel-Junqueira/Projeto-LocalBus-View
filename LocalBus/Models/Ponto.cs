using System.Linq;
using System.ComponentModel.DataAnnotations;
using LocalBus.Context;

namespace LocalBus.Models
{
   public class Ponto
    {
        
        public int PontoId { get; set; }

        [Required(ErrorMessage = "Insira a Latitude do Ponto")]
        public double latitudePonto { get; set; }

        [Required(ErrorMessage = "Insira a Longitude do Ponto")]
        public double LongitudePonto { get; set; }
        public bool AtivoPonto { get; set; }
        public string DescriçãoPonto { get; set; }

        [Required(ErrorMessage = "Insira o nome do ponto")]
        public string Nome { get; set; }

        public ICollection<EscolaPonto> Escola_Ponto { get; set; }
    
    }
}
