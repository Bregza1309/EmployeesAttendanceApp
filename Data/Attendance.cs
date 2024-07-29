namespace Employees.Common.DataContext.SqlServer
{
    public class Attendance
    { 
        public int Id { get; set; }
        public DateTime ClockInTime { get; set; } = DateTime.Now;
        public DateTime ClockOutTime { get; set; }
        public int EmployeeId { get; set; }
        public double HoursWorked { get; set; }
        public Employee Employee { get; set; } = new();
        public double? Wage { get; set; }
    }
}
