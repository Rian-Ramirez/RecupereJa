using System.ComponentModel.DataAnnotations;

namespace RecupereJa.ViewModel
{
    public class PerfilUsuarioViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        
        public string Cidade { get; set; }
        
        public string Telefone { get; set; }
        
        public string Email { get; set; }
        
        public string Endereço { get; set; }
        
        public string Genero { get; set; }

        [Display(Name = "Foto de usuário")]
        public byte[] FotoPerfilUrl { get; set; }

        public double Rating { get; set; }
        
        public DateTime Nascimento{ get; set; }
        
        public string DataNascimentoFormatada => Nascimento.ToString("dd/MM/yyyy");
    }

}
