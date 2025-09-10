using System.ComponentModel.DataAnnotations;
using RecupereJa.Enums;

namespace RecupereJa.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O identificador é obrigatório")]
        [MaxLength(100)]
        public string Identificador { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MaxLength(200)]
        public string Senha { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Telefone { get; set; }

        public CargoEnum Cargo { get; set; } = CargoEnum.Usuario;

        // Foto do usuário
        public byte[]? FotoUsuario { get; set; }

        [MaxLength(50)]
        public string? FotoUsuarioMimeType { get; set; }

        // Propriedade auxiliar para exibir a imagem em Base64
        public string? FotoPerfilBase64 =>
            FotoUsuario != null && FotoUsuario.Length > 0
            ? $"data:{FotoUsuarioMimeType ?? "image/png"};base64," + Convert.ToBase64String(FotoUsuario)
            : null;
    }
}
