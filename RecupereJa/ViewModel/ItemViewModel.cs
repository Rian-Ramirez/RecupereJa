using System.ComponentModel.DataAnnotations;
using RecupereJa.Models;
using RecupereJa.Enums;

namespace RecupereJa.ViewModel
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(150, ErrorMessage = "O título deve ter no máximo 150 caracteres")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        [Display(Name = "Data que foi encontrado")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]
        public DateTime? DataEncontrado { get; set; }

        [Display(Name = "Status do item")]
        [Required(ErrorMessage = "O status é obrigatório")]
        public ItemStatusEnum Status { get; set; } = ItemStatusEnum.Perdido;

        [Display(Name = "Imagem")]
        public string? ImagemUrl { get; set; }

        [Display(Name = "Usuário que cadastrou")]
        public int IdUsuario { get; set; }

        public bool TemDescricao => !string.IsNullOrEmpty(Descricao);

        // Conversão de ViewModel para o Model
        public static explicit operator Item(ItemViewModel? vm)
        {
            if (vm == null) return null!;

            return new Item
            {
                Id = vm.Id,
                Titulo = vm.Titulo,
                Descricao = vm.Descricao,
                Status = vm.Status,
                DataEncontrado = vm.DataEncontrado,
                ImagemUrl = vm.ImagemUrl,
                IdUsuario = vm.IdUsuario,
                DataCriacao = DateTime.UtcNow, 
                Ativo = true,                  
                Aprovado = false               
            };
        }

        // Conversão de Model  para o ViewModel
        public static ItemViewModel FromItem(Item? i)
        {
            if (i == null) return null!; 

            return new ItemViewModel
            {
                Id = i.Id,
                Titulo = i.Titulo,
                Descricao = i.Descricao,
                DataEncontrado = i.DataEncontrado,
                Status = i.Status,
                ImagemUrl = i.ImagemUrl,
                IdUsuario = i.IdUsuario
            };
        }
    }
}
