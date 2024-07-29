using Microsoft.AspNetCore.Http.HttpResults; // To use Results.
using Microsoft.AspNetCore.Mvc; // To use [FromServices] and so on.
using Employees.Common.DataContext.SqlServer;
using EmployeesAttendanceApp.Repositories;
namespace EmployeesAttendanceApp.Extensions
{
    public  static partial class EmployeeAppExtensions
    {
        //map the put method(s) to the application
        public static WebApplication MapPuts(this WebApplication app)
        {
            app.MapPut("api/employees/{id:int}" , async ([FromBody] Employee employee , [FromRoute] int id , [FromServices] IEmployeeCrud employeeCrud) =>
            {
                var succeeded = await employeeCrud.UpdateEmployeeAsync(employee, id);
                if (!succeeded) return Results.NotFound();
                return Results.NoContent();
            }).WithOpenApi()
          .Produces(StatusCodes.Status404NotFound)
          .Produces(StatusCodes.Status204NoContent);

            //Employee clockOut put method
            app.MapPut("/api/employees/out/{id:int}", async Task<Results<Ok<double>, NotFound>> ([FromRoute] int id,
               [FromServices] IAttendanceCrud attendanceCrud) =>
            {
                var succeeded = await attendanceCrud.EmployeeClockOutAsync(id);
                return succeeded != 0 ? TypedResults.Ok(succeeded) : TypedResults.NotFound();
            })
               .WithOpenApi()
               .Produces<double>(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status404NotFound);
            return app;
        }

    }
}
