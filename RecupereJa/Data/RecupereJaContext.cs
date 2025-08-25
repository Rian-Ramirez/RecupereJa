using Microsoft.EntityFrameworkCore;
using RecupereJa.Models;

namespace RecupereJa.Data
{
    public class RecupereJaContext : DbContext
    {
        public RecupereJaContext(DbContextOptions<RecupereJaContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; } = null!;

        public DbSet<Item> Items { get; set; } = null!;
    }
}





//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<Item>(entity =>
//    {
//        entity.HasKey(e => e.Id);
//        entity.Property(e => e.Titulo).IsRequired().HasMaxLength(100);
//        entity.Property(e => e.Descricao).HasMaxLength(500);
//    });
//}