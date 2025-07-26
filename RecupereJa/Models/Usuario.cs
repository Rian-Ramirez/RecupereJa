using System.ComponentModel.DataAnnotations;

namespace RecupereJa.Models

{
    public class Usuario
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "O nome é obrigatório ")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Insira um e-mail válido")]
        public string Email {  get; set; }


        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "A senha deve ter de 8 a 30 caracteres.")]
        public string Senha { get; set; }
        
    }
}
