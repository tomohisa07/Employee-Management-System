using Microsoft.EntityFrameworkCore;

namespace EmsApi.Models
{
    public class EmsContext : DbContext
    {
        public EmsContext(DbContextOptions<EmsContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; } = null!;
        public DbSet<Employee> Employee { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;

    }
}
