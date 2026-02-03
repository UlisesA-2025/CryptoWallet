using CryptoWallet.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoWallet.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(e =>
            {
                e.ToTable("transactions");
                e.HasKey(x => x.Id);

                e.Property(x => x.CryptoCode).IsRequired();
                e.Property(x => x.Action).IsRequired();

                // SQLite guarda decimales como REAL
                e.Property(x => x.CryptoAmount).HasColumnType("REAL");
                e.Property(x => x.Money).HasColumnType("REAL");

                e.Property(x => x.DateTime).IsRequired();
            });
        }
    }
}
