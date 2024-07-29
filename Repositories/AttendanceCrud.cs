
using Employees.Common.DataContext.SqlServer;
namespace EmployeesAttendanceApp.Repositories
{
    public class AttendanceCrud(EmployeesContext context , IEmployeeCrud employeeCrud) : IAttendanceCrud
    {
        //inject the dbContext from the services container
        readonly EmployeesContext dbContext = context;
        readonly IEmployeeCrud employeeCrud = employeeCrud;

        //calculate the average working hours rate of all the employees
        public async Task<double> AverageWorkingHoursAsync()
        {
            var average =  dbContext.Attendances
                .Where(a => a.Wage != null)
                .Average(a => a.HoursWorked);
            return await Task.FromResult(average);
        }

        public async Task<bool> EmployeeClockInAsync(int EmployeeId)
        {
            var employee = await employeeCrud.GetEmployeeByIdAsync(EmployeeId);
            if(employee != null && !employee.ClockedIn )
            {
                //add a new attendance to keep track of ckockIn and clockOut times 
                var employeeAttendance = new Attendance { EmployeeId = EmployeeId, ClockInTime = DateTime.Now };
                employee.Attendances.Add(employeeAttendance);
                //update the checkedIn property of the employee
                employee.ClockedIn = true;
                await employeeCrud.UpdateEmployeeAsync(employee, EmployeeId);
                return true;
            }
            return false;
        }


        public async Task<double> EmployeeClockOutAsync(int EmployeeId)
        {
            var employee = await employeeCrud.GetEmployeeByIdAsync(EmployeeId);
            //check if the employee is clockedIn
            if (employee != null && employee.ClockedIn)
            {
                
                
                //get the latest attendance record from the db
                var latestAttendance = employee.Attendances.Last();
                latestAttendance.ClockOutTime = DateTime.Now;
                //calculate hours worked 
                TimeSpan duration = latestAttendance.ClockOutTime - latestAttendance.ClockInTime;
                latestAttendance.HoursWorked = duration.TotalHours;
                //calculate daily wage  
                latestAttendance.Wage = employee.RatePerHour * latestAttendance.HoursWorked;
                dbContext.Attendances.Update(latestAttendance);
                await dbContext.SaveChangesAsync();
                //update the employee clockedInStatus
                employee.ClockedIn = false;
                await employeeCrud.UpdateEmployeeAsync(employee, EmployeeId);
                //return daily wage
                return latestAttendance.Wage ?? 0;
            }
            return 0;
        }
    }
}
