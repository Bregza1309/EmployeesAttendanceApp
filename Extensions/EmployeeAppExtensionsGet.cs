using Microsoft.AspNetCore.Http.HttpResults; // To use Results.
using Microsoft.AspNetCore.Mvc; // To use [FromServices] and so on.
using Employees.Common.DataContext.SqlServer;
using EmployeesAttendanceApp.Repositories;
namespace EmployeesAttendanceApp.Extensions
{
    public  static partial class EmployeeAppExtensions
    {
        //map the get method(s) to the application
        public static WebApplication MapGets(this WebApplication app)
        {
            //Get :: '/api/employees'
            app.MapGet("/api/employees", async ([FromServices] IEmployeeCrud employeeCrud) =>
            {
                //get a list of employees from the repository asynchronously 
                var employees = await employeeCrud.GetEmployeesAsync();
                return employees;
            }).WithName("GetEmployees")
            .WithOpenApi()
            .Produces<Employee[]>(StatusCodes.Status200OK);

            //Get :: '/api/employees/id'
            app.MapGet("/api/employees/{id:int}" ,  async Task<Results<Ok<Employee>, NotFound>> 
                ([FromServices] IEmployeeCrud employeeCrud, [FromRoute] int id) =>
            {
                var employee = await employeeCrud.GetEmployeeByIdAsync(id);
                //return the requested employeed if found or NotFound status code if not found
                return employee != null ? TypedResults.Ok(employee) : TypedResults.NotFound();

            }
            ).WithName("GetEmployeeById")
                .WithOpenApi()
                .Produces<Employee>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            //Get the average rate hours of all employees
            app.MapGet("/api/employees/averagehrsrate" ,  async ([FromServices] IAttendanceCrud attendanceCrud) =>
            {
                var result = await attendanceCrud.AverageWorkingHoursAsync();
                return TypedResults.Ok(result);
            }).WithOpenApi()
            .Produces<double>(StatusCodes.Status200OK);
            return app;
        }

    }
}
