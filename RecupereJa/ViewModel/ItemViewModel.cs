using RecupereJa.Enums;
using RecupereJa.Models;

namespace RecupereJa.ViewModel
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string UsuarioNome { get; set; } = string.Empty; // usado na View
        public bool Aprovado { get; set; }
        public ItemStatusEnum Status { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataEncontrado { get; set; }
        public string? ImagemUrl { get; set; }

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
                // UsuarioId é preenchido no controller com ObterUsuarioLogadoId()
            };
        }
    }
}
