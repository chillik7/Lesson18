using EmployeeManager.Models;
using System.Data.Entity;

namespace EmployeeManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
