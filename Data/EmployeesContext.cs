using Microsoft.EntityFrameworkCore;
namespace Employees.Common.DataContext.SqlServer
{
    public class EmployeesContext:DbContext
    {
        public EmployeesContext(DbContextOptions<EmployeesContext> opts) : base(opts) { }
        public DbSet<Employee> Employees { get; set; } 
        public DbSet<Attendance> Attendances { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Navigation(e => e.Attendances)
                .AutoInclude();
            modelBuilder.Entity<Attendance>()
                .Navigation(a => a.Employee)
                .AutoInclude();
            
        }
        
    }
}
