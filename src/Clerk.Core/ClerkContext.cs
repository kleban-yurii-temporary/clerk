using Clerk.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clerk.Core
{
    public class ClerkContext : IdentityDbContext<User>
    {
        public ClerkContext(DbContextOptions<ClerkContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.SeedFakeData();
            base.OnModelCreating(builder);
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<AttachedFile> AttachedFiles { get; set; }
        public DbSet<AttachedMonthlyFiles> AttachedMonthlyFiles { get; set; }
        public DbSet<PersonOnPosition> PersonsOnPositions { get; set; }
        public DbSet<SalaryComponent> SalaryComponents { get; set; }
        public DbSet<MonthlyPayment> MonthlyPayments { get; set; }
    }
}