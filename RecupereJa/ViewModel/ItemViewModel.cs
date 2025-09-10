using RecupereJa.Enums;
using RecupereJa.Models;
using System.ComponentModel.DataAnnotations;

namespace RecupereJa.ViewModel
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string UsuarioNome { get; set; } = string.Empty; // usado na View
        public bool Aprovado { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataEncontrado { get; set; }

        [Display(Name = "Status do item")]
        [Required(ErrorMessage = "O status é obrigatório")]
        public ItemStatusEnum Status { get; set; } = ItemStatusEnum.Perdido;

        [Display(Name = "Imagem")]
        public string? ImagemUrl { get; set; }

        [Display(Name = "Usuário que cadastrou")]
        public int IdUsuario { get; set; }

        public bool TemDescricao => !string.IsNullOrEmpty(Descricao);

        // Conversão de Item -> ItemViewModel
        public static ItemViewModel FromItem(Item item)
        {
            return new ItemViewModel
            {
                Id = item.Id,
                Titulo = item.Titulo,
                Descricao = item.Descricao,
                UsuarioNome = item.Usuario?.Nome ?? "Usuário não informado", // garante que nunca vem vazio
                Aprovado = item.Aprovado,
                Status = item.Status,
                Ativo = item.Ativo,
                DataCriacao = item.DataCriacao,
                DataEncontrado = item.DataEncontrado,
                ImagemUrl = item.ImagemUrl
            };
        }

        // Conversão inversa se precisar (ViewModel -> Model)
        public static explicit operator Item(ItemViewModel vm)
        {
            return new Item
            {
                Id = vm.Id,
                Titulo = vm.Titulo,
                Descricao = vm.Descricao,
                Aprovado = vm.Aprovado,
                Status = vm.Status,
                Ativo = vm.Ativo,
                DataCriacao = vm.DataCriacao,
                DataEncontrado = vm.DataEncontrado,
                ImagemUrl = vm.ImagemUrl,
                IdUsuario = vm.IdUsuario,
                //DataCriacao = DateTime.UtcNow,           
            };
        }

        //// Conversão de Model  para o ViewModel
        //public static ItemViewModel FromItem(Item? i)
        //{
        //    if (i == null) return null!; 

        //    return new ItemViewModel
        //    {
        //        Id = i.Id,
        //        Titulo = i.Titulo,
        //        Descricao = i.Descricao,
        //        DataEncontrado = i.DataEncontrado,
        //        Status = i.Status,
        //        ImagemUrl = i.ImagemUrl,
        //        IdUsuario = i.IdUsuario
        //        // UsuarioId é preenchido no controller com ObterUsuarioLogadoId()
    };
}
