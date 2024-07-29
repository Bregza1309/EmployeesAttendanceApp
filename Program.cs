/*
    I  implemented the Employee App Task  using Minial Web Api instead of controllers
    
    You can use dotnet-ef commands to create and update the database tables and columns 
    Also used MSSQL Server ( Developer Edition ) for the database check out the appsettings.json for the connection string

    Thank you for your time !!!!!!!!
 */
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http.Json;
using EmployeesAttendanceApp.Extensions;
using System.Text.Json.Serialization;//AddEmployeeServices , AddEmployeesContext , MapGets , MapPuts, MapPosts ,MapDeletes
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//configure the json serializer for navigated properties
builder.Services.Configure<JsonOptions>(opts => 
opts.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
//add employeecontext and employee services to the service container through extension methods
builder.Services.AddEmployeesContext(builder.Configuration.GetConnectionString("EmployeeDbConnection") ?? string.Empty);
builder.Services.AddEmployeeServices();

//add the employee context extension method 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//add the EmployeeAppExtensions
app.
    MapGets().
    MapPosts()
    .MapPuts()
    .MapDeletes();

app.Run();


