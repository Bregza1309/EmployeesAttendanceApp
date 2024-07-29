using Microsoft.AspNetCore.Http.HttpResults; // To use Results.
using Microsoft.AspNetCore.Mvc; // To use [FromServices] and so on.
using Employees.Common.DataContext.SqlServer;
using EmployeesAttendanceApp.Repositories;
namespace EmployeesAttendanceApp.Extensions
{
    public static partial class EmployeeAppExtensions
    {
        //adds the Employee CrudOperations and Business Logic to the service container
        public static IServiceCollection AddEmployeeServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeCrud, EmployeeCrud>();
            services.AddScoped<IAttendanceCrud, AttendanceCrud>();
            return services;
        }
    }
}
