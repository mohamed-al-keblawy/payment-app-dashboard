using Microsoft.EntityFrameworkCore;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Card> Cards => Set<Card>();
}
