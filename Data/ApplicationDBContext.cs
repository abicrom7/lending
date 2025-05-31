using Lending.Models;
using Microsoft.EntityFrameworkCore;

namespace Lending.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Borrower>().ToTable("Borrowers");
            modelBuilder.Entity<Collector>().ToTable("Collectors");
            modelBuilder.Entity<Interest>().ToTable("Interests");
            modelBuilder.Entity<Loan>().ToTable("Loans");

            modelBuilder.Entity<Loan>()
                .HasIndex(l => l.LoanReferenceNumber)
                .IsUnique();

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.Property(e => e.MonthlyPay).HasPrecision(18, 2);
                entity.Property(e => e.PrincipalAmount).HasPrecision(18, 2);
                entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
            });
        }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Collector> Collectors { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Term> Terms { get; set; }
    }
}
