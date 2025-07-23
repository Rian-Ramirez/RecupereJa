using Microsoft.EntityFrameworkCore;

using RecupereJa.Models;

namespace RecupereJa.Repository
{
    public class ItemControler : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options)
            : base(options) { }

        public DbSet<Item> Item { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).HasMaxLength(500);
                //entity.Property(e => e.DataCriacao).IsRequired();
                //entity.Property(e => e.Prioridade).HasConversion<int>();
            });
        }
    }
}
