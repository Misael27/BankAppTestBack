using BankAppTestBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Infrastructure
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        #region DbSets
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Movement> Movements { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Clients");
                entity.HasIndex(c => c.PersonId).IsUnique();

                entity.Property(c => c.PersonId).HasColumnName("person_id").IsRequired();
                entity.Property(c => c.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                entity.Property(c => c.Password).HasColumnName("password").IsRequired();
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Accounts");
                entity.Property(a => a.Number).HasColumnName("number").IsRequired();
                entity.Property(a => a.InitBalance).HasColumnName("init_balance");

                entity.HasOne(a => a.Client)
                    .WithMany(c => c.Accounts)
                    .HasForeignKey(a => a.ClientId);

                entity.Property(a => a.Type)
                    .HasConversion<string>()
                    .HasColumnName("type")
                    .IsRequired();
            });

            modelBuilder.Entity<Movement>(entity =>
            {
                entity.ToTable("Movements");
                entity.Property(m => m.Date).HasColumnName("date").IsRequired();

                entity.Property(m => m.Type)
                    .HasConversion<string>()
                    .HasColumnName("type")
                    .IsRequired();

                entity.HasOne(m => m.Account)
                    .WithMany(a => a.Movements)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasForeignKey(m => m.AccountId);
            });
        }
    }
}
