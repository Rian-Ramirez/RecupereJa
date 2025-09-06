using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecupereJa.Enums;

namespace RecupereJa.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Titulo { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        [ForeignKey(nameof(Usuario))]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public bool Aprovado { get; set; } = false;

        [Required]
        public ItemStatusEnum Status { get; set; } = ItemStatusEnum.Perdido;

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public DateTime? DataEncontrado { get; set; }

        public string? ImagemUrl { get; set; }
    }
}
