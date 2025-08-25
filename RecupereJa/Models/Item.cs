using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecupereJa.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public string? Descricao { get; set; }


        [ForeignKey(nameof(Usuario))] //Não colocar acento em usuário, vai quebrar
        public int IdUsuario { get; set; }


        public Usuario Usuario { get; set; } = null!;

        public bool Status { get; set; } = false;
        
        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;


        [Required]
        [MaxLength(150)]
        public string Titulo { get; set; } = string.Empty;


        public DateTime? DataEncontrado { get; set; }
    }
}