using Employees.Common.DataContext.SqlServer;
namespace EmployeesAttendanceApp.Repositories
{
    public interface IEmployeeCrud
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int Id );
        Task<bool> UpdateEmployeeAsync( Employee employee ,int Id );
        Task<bool> DeleteEmployeeAsync( int Id );
        Task AddEmployeeAsync(Employee employee );
    }
}
