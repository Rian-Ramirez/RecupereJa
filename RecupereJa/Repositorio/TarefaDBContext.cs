using Microsoft.EntityFrameworkCore;

using RecupereJa.Models;

namespace RecupereJa.Repository
{
    public class TarefaContext : DbContext
    {
        public TarefaContext(DbContextOptions<TarefaContext> options)
            : base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).HasMaxLength(500);
                entity.Property(e => e.DataCriacao).IsRequired();
                entity.Property(e => e.Prioridade).HasConversion<int>();
            });
        }
    }
}
