using Employees.Common.DataContext.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAttendanceApp.Repositories
{
    public class EmployeeCrud(EmployeesContext context) : IEmployeeCrud
    {
        readonly EmployeesContext dbContext = context;
        public async Task AddEmployeeAsync(Employee employee)
        {
            dbContext.Employees.Add(employee);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(int Id)
        {
            var employee = await  dbContext.Employees.FindAsync(Id);
            if(employee != null)
            {
                dbContext.Employees.Remove(employee);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int Id)
        {
            var employee = await dbContext.Employees.FindAsync(Id);
            return employee;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await dbContext.Employees.ToListAsync();
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee, int Id)
        {
            var existingEmployee  = await dbContext.Employees.FindAsync(Id);
            if(existingEmployee != null)
            {
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.RatePerHour = employee.RatePerHour;
                dbContext.Employees.Update(existingEmployee);
                dbContext.SaveChanges();
                return true;
            }
            return false;

        }
    }
}
