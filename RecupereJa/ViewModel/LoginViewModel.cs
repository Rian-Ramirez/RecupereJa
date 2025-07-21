using System.ComponentModel.DataAnnotations;

namespace RecupereJa.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Nome de usuário é obrigatório")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
