using System.Linq;
using System.ComponentModel.DataAnnotations;
namespace LocalBus.Models
{
    public class Ponto
    {
        public int PontoId { get; set; }

        [Required(ErrorMessage ="Insira a Latitude do Ponto")]
        public double latitudePonto { get; set; }

        [Required(ErrorMessage = "Insira a Longitude do Ponto")]
        public double LongitudePonto { get; set; }
        public bool AtivoPonto { get; set; }
        public string DescriçãoPonto { get; set; }

        public ICollection<EscolaPonto> Escolas_Ponto { get; set; }
    }
}
