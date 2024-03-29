using capstone.Models;

namespace capstone.Data;
using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
    public DbSet<Accountant>? Accountants { get; set; }
    public DbSet<Loan>? Loans { get; set; }
    public DbSet<User>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Loan>().HasKey(x => x.LoanId );
        modelBuilder.Entity<Accountant>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().HasKey(x => x.Id);
    }
}