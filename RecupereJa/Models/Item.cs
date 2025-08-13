using System.Diagnostics.Eventing.Reader;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RecupereJa.Models
{
    public class Item
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; }


        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Descricao {  get; set; }

        [Display(Name = "Profile Picture")]
        public byte[] ImagemObjeto { get; set; }


        [Required(ErrorMessage = "A data é obrigatória.")]
        public DateTime DataEncontrado { get; set; }


        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(50)]
        public string Cidade {  get; set; }


        public bool Status {  get; set; }


        [ForeignKey("Usuário")]
        public int IdUsuario {  get; set; }


        public Usuario? Usuario { get; set; }
    }
}
