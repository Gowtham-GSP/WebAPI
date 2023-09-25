using Employees.Model;
using Microsoft.EntityFrameworkCore;

namespace Employees.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }

       public DbSet<Employee> Employees { get; set; }
    }
}
