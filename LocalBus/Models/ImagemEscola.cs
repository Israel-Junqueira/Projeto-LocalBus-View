using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LocalBus.Models
{
    public class ImagemEscola
    {
        public int ImagemEscolaId { get; set; }
        public int EscolaId { get; set; }
        public Escola Escola { get; set; }
        public int TipoImagemId { get; set; }
        public TipoImagem TipoImagem{ get; set; }

      
    }

}
