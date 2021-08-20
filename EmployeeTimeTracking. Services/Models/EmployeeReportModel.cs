using System;

namespace EmployeeTimeTracking.Services.Models
{
    public class EmployeeReportModel
    {
        public string FullNameEmployee { get; set; }
        public string PositionEmployee { get; set; }
        public int NumberOfHour { get; set; }
        public bool Recycling { get; set; }
        public DateTime Date { get; set; }
        public string DescriptionWork { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ReportId { get; set; }
    }
}
