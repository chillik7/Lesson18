using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }

        public decimal Salary { get; set; }
    }
}
