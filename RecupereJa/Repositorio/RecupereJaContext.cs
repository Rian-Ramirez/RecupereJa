using Microsoft.EntityFrameworkCore;

using RecupereJa.Models;

namespace RecupereJa.Repository
{
    public class RecupereJaContext : DbContext
    {
        public RecupereJaContext(DbContextOptions<RecupereJaContext> options)
            : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).HasMaxLength(500);
            });
        }
    }
}
