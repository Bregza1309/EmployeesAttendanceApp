using Microsoft.EntityFrameworkCore;//useSqlSercer
using Employees.Common.DataContext.SqlServer;
namespace EmployeesAttendanceApp.Extensions
{
    public static class EmployeeContextExtensions
    {
        public static IServiceCollection AddEmployeesContext(this IServiceCollection services , string connectionString)
        {
            services.AddDbContext<EmployeesContext>(opts =>
            {
                opts.UseSqlServer(connectionString);
                //add a transient service lifetime to fix concurrency issues
            }, optionsLifetime: ServiceLifetime.Transient);
            return services;
        }
    }
}
