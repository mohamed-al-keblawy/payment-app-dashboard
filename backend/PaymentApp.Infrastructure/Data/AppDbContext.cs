using Microsoft.EntityFrameworkCore;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Card> Cards => Set<Card>();

    public DbSet<PaymentTransaction> Transactions => Set<PaymentTransaction>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Card>().HasData(
            new Card
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CardNumber = "1234567890123456",
                CardHolder = "Mohamed Al Keblawy",
                ExpiryDate = new DateTime(2026, 12, 31),
                Balance = 1000
            }
        );
    }


}
