using System.ComponentModel.DataAnnotations;
using RecupereJa.Enums;

namespace RecupereJa.Models
{
    public class Usuario
    {
        [Key] // Define como chave primária
        public int Id { get; set; }


        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(150, ErrorMessage = "O nome pode ter no máximo 150 caracteres")]
        public string Nome { get; set; } = string.Empty;


        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
        [MaxLength(150, ErrorMessage = "O e-mail pode ter no máximo 150 caracteres")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "O identificador é obrigatório")]
        [MaxLength(100, ErrorMessage = "O identificador pode ter no máximo 100 caracteres")]
        public string Identificador { get; set; } = string.Empty;


        [Required(ErrorMessage = "A senha é obrigatória")]
        [MaxLength(200, ErrorMessage = "A senha pode ter no máximo 200 caracteres")]
        public string Senha { get; set; } = string.Empty;


        //Define por padrão como usuário
        public CargoEnum Cargo { get; set; } = CargoEnum.Usuario;

        public byte[]? FotoUsuario { get; set; }
    }
}