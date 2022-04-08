using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace LocalBus.Models
{
    public class TipoImagem 
    {
        public int TipoImagemId { get; set; }

        public string Nome { get; set; }
        public string Extencao { get; set; }
        public string Descricao { get; set; }
        public byte[] Picture { get; set; }
        public int Length { get; set; }
        public string ContentType { get; set; }
        public ICollection<ImagemEscola> ImagemEscola { get; set; }
    }
}
