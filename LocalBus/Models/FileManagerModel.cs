namespace LocalBus.Models
{
    public class FileManagerModel
    {
        public FileInfo[] Files { get; set; }//ele da metodos e propriedades para tratar os arquivos
        public IFormFile IFormFile { get; set; }//permite o upload dos arquivos 
        public List<IFormFile> IFormFiles { get; set; }
        public string PathImagesEscolas { get; set; }
        
    }
}
