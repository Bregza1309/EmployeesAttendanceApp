using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Employees.Common.DataContext.SqlServer
{
    public class Employee
    {
        public int EmployeeId {  get; set; }
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        public double RatePerHour { get; set; }
        public bool ClockedIn {  get; set; } = false;
        
        public virtual  ICollection<Attendance> Attendances { get; set; } = [];
    }
}
