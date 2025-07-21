using System.Diagnostics.Eventing.Reader;

namespace RecupereJa.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string? Descricao {  get; set; } 
        public string? ImagemUrl { get; set; }
        public DateTime DataEncontrado { get; set; }
        public string Cidade {  get; set; }
        public bool Status {  get; set; }
        public int IdUsuario {  get; set; }
    }
}
