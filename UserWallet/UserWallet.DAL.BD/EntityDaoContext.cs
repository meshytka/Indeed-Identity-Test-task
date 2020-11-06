using Microsoft.EntityFrameworkCore;
using UserWallet.Entities;

namespace UserWallet.DAL.BD
{
    public class EntityDaoContext : DbContext
    {
        public virtual DbSet<Wallet> Wallets { get; set; }

        public EntityDaoContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Wallet>().HasKey(wallet => new
            {
                wallet.ClientId
            });

            builder.Entity<CurrencyAccount>()
                .HasOne(c => c.Wallet)
                .WithMany(w => w.CurrencyAccounts)
                .HasForeignKey(c => c.WalletId);
        }
    }
}