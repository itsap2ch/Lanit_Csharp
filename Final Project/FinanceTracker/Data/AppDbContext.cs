using Microsoft.EntityFrameworkCore;
using FinanceTracker.Models;

namespace FinanceTracker.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();

    public DbSet<ExpenseItem> ExpenseItems => Set<ExpenseItem>();

    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>()
            .HasMany(c => c.ExpenseItems)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ExpenseItem>()
            .HasMany(e => e.Transactions)
            .WithOne(t => t.ExpenseItem)
            .HasForeignKey(t => t.ExpenseItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}