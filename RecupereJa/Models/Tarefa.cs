using System.ComponentModel.DataAnnotations;

using RecupereJa.Enums;
using RecupereJa.Services;
using RecupereJa.ViewModels;

namespace RecupereJa.Models
{
    public class Tarefa
    {
        public Tarefa()
        {
            DataCriacao = DateTime.Now;
            Concluida = false;
            Prioridade = PrioridadeEnum.Media;
        }

        public int Id { get; set; }
        public string Titulo { get; set; }

        public string? Descricao { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataConclusao { get; set; }

        public bool Concluida { get; set; }

        public PrioridadeEnum Prioridade { get; set; }  
    }
}
