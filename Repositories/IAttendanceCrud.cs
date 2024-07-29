using Employees.Common.DataContext.SqlServer;
namespace EmployeesAttendanceApp.Repositories
{
    public interface IAttendanceCrud
    {
        Task<bool> EmployeeClockInAsync(int EmployeeId);
        Task<double> EmployeeClockOutAsync(int EmployeeId);
        Task<double> AverageWorkingHoursAsync();
        
    }
}
