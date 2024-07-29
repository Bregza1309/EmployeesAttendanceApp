using Microsoft.AspNetCore.Http.HttpResults; // To use Results.
using Microsoft.AspNetCore.Mvc; // To use [FromServices] and so on.
using Employees.Common.DataContext.SqlServer;
using EmployeesAttendanceApp.Repositories;
namespace EmployeesAttendanceApp.Extensions
{
    public static partial class EmployeeAppExtensions
    {
       public static WebApplication MapDeletes(this WebApplication app)
        {
            app.MapDelete("/api/employees/{id:int}" , async ([FromServices] IEmployeeCrud employeeCrud , [FromRoute] int id) =>
            {
                var succeeded = await employeeCrud.DeleteEmployeeAsync(id);
                if(!succeeded) return Results.NotFound();
                return Results.NoContent();
            })
                .WithOpenApi()
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound);
            return app;
        }
    }
}
