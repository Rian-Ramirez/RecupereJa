using System.ComponentModel.DataAnnotations;

namespace RecupereJa.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Nome de usuário ou E-mail é obrigatório")]
        public string? Identificador { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }
    }
}
