using System.ComponentModel.DataAnnotations;
using RecupereJa.Models;

namespace RecupereJa.ViewModel
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A data é obrigatória")]
        [Display(Name = "Data que foi encontrado")]
        public string DataEncontrado { get; set; }

        [Display(Name = "Está com o dono?")]
        public bool Status { get; set; }

        public bool TemDescricao => !string.IsNullOrEmpty(Descricao);

        public static explicit operator Item(ItemViewModel viewModel)
        {
            if (viewModel == null) return null;

            var item = new Item
            {
                Id = viewModel.Id,
                Titulo = viewModel.Titulo,
                Descricao = viewModel.Descricao,
                Status = viewModel.Status
            };

            if (DateTime.TryParseExact(viewModel.DataEncontrado, "dd/MM/yyyy HH:mm",
                                       null, System.Globalization.DateTimeStyles.None,
                                       out DateTime dataEncontrado))
            {
                item.DataEncontrado = dataEncontrado;
            }
            else
            {
                item.DataEncontrado = DateTime.Now;
            }

            return item;
        }

        public static ItemViewModel FromItem(Item item)
        {
            if (item == null) return null;

            return new ItemViewModel
            {
                Id = item.Id,
                Titulo = item.Titulo,
                Descricao = item.Descricao,
                DataEncontrado = item.DataEncontrado?.ToString("dd/MM/yyyy HH:mm") ?? string.Empty,
                Status = item.Status
            };
        }
    }
}