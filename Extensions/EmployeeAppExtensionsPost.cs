using Microsoft.AspNetCore.Http.HttpResults; // To use Results.
using Microsoft.AspNetCore.Mvc; // To use [FromServices] and so on.
using Employees.Common.DataContext.SqlServer;
using EmployeesAttendanceApp.Repositories;
namespace EmployeesAttendanceApp.Extensions
{
    public  static partial class EmployeeAppExtensions
    {
       
        
        //map the post method(s) to the application
        public static WebApplication MapPosts(this WebApplication app)
        {
            app.MapPost(("/api/employees"), async ([FromBody] Employee employee , [FromServices] IEmployeeCrud employeeCrud) =>
            {
                //add employee and return a link to the newly added employee
                await employeeCrud.AddEmployeeAsync(employee);
                return Results.Created($"api/employees/{employee.EmployeeId}" , employee);
            }).WithOpenApi()
              .Produces<Employee>(StatusCodes.Status201Created);

            //Employee clockIn post method
            app.MapPost("/api/employees/in/{id:int}" , async Task<Results<Ok, NotFound>> ([FromRoute] int id , 
                [FromServices] IAttendanceCrud attendanceCrud) =>
            {
                var succeeded = await attendanceCrud.EmployeeClockInAsync(id);
                return succeeded ? TypedResults.Ok() : TypedResults.NotFound();
            })
                .WithOpenApi()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);
           
            
            return app;

        }

        


    }
}
