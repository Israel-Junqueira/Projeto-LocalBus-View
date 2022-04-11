namespace LocalBus.Models
{
    public class EscolaPonto
    {
        public int EscolaPontoId { get; set; }
        public int EscolaId { get; set; }
        public Escola Escola { get; set; }

        public Ponto Ponto { get; set; }
        public int PontoId { get; set; }
    }
}
