using System.ComponentModel.DataAnnotations;

using RecupereJa.Enums;
using RecupereJa.Models;

namespace RecupereJa.ViewModels;
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

    [Display(Name = "Data de Criação")]
    public string DataCriacao { get; set; }

    [Display(Name = "Data de Conclusão")]
    public string? DataConclusao { get; set; }

    [Display(Name = "Concluída")]
    public bool Concluida { get; set; }

    [Display(Name = "Prioridade")]
    public PrioridadeEnum Prioridade { get; set; }

    // Propriedades auxiliares para exibição                     69887098
    public string? PrioridadeTexto { get; set; }
    public string? PrioridadeCor { get; set; }
    public string? StatusTexto { get; set; }
    public string? StatusCor { get; set; }
    public bool TemDescricao { get; set; }

    // Operador de conversão explícita para Tarefa
    public static explicit operator Item(ItemViewModel viewModel)
    {
        if (viewModel == null)
            return null;

        var item = new Item
        {
            Id = viewModel.Id,
            Titulo = viewModel.Titulo,
            Descricao = viewModel.Descricao,
            //Concluida = viewModel.Concluida,
            //Prioridade = viewModel.Prioridade
        };

        // Conversão das datas de string para DateTime
        if (!string.IsNullOrEmpty(viewModel.DataCriacao) &&
            DateTime.TryParseExact(viewModel.DataCriacao, "dd/MM/yyyy HH:mm",
                                 null, System.Globalization.DateTimeStyles.None,
                                 out DateTime dataCriacao))
        {
            //item.DataCriacao = dataCriacao;
        }
        else
        {
            // Se não conseguir converter, usa data atual
            //item.DataCriacao = DateTime.Now;
        }

        // Conversão da data de conclusão
        if (!string.IsNullOrEmpty(viewModel.DataConclusao) &&
            DateTime.TryParseExact(viewModel.DataConclusao, "dd/MM/yyyy HH:mm",
                                 null, System.Globalization.DateTimeStyles.None,
                                 out DateTime dataConclusao))
        {
            //item.DataEncontrado = dataConclusao;
        }
        else if (viewModel.Concluida)
        {
            // Se está marcada como concluída mas não tem data, define agora
            //item.DataConclusao = DateTime.Now;
        }

        return item;
    }

    // Método auxiliar para criar ViewModel a partir de Tarefa
    public static ItemViewModel FromItem(Item item)
    {
        if (item == null)
            return null;

        return new ItemViewModel
        {
            Id = item.Id,
            Titulo = item.Titulo,
            Descricao = item.Descricao,
            //DataCriacao = item.DataCriacao.ToString("dd/MM/yyyy HH:mm"),
            //DataConclusao = item.DataConclusao?.ToString("dd/MM/yyyy HH:mm"),
            //Concluida = item.Concluida,
            //Prioridade = item.Prioridade,
            //PrioridadeTexto = GetPrioridadeTexto(item.Prioridade),
            //PrioridadeCor = GetPrioridadeCor(item.Prioridade),
            //StatusTexto = item.Concluida ? "Concluída" : "Pendente",
            //StatusCor = item.Concluida ? "success" : "primary",
            TemDescricao = !string.IsNullOrEmpty(item.Descricao)
        };
    }

    //private static string GetPrioridadeTexto(PrioridadeEnum prioridade)
    //{
    //    return prioridade switch
    //    {
    //        PrioridadeEnum.Baixa => "Baixa",
    //        PrioridadeEnum.Media => "Média",
    //        PrioridadeEnum.Alta => "Alta",
    //        _ => "Média"
    //    };
    //}

    //private static string GetPrioridadeCor(PrioridadeEnum prioridade)
    //{
    //    return prioridade switch
    //    {
    //        PrioridadeEnum.Baixa => "secondary",
    //        PrioridadeEnum.Media => "warning",
    //        PrioridadeEnum.Alta => "danger",
    //        _ => "warning"
    //    };
    //}
}