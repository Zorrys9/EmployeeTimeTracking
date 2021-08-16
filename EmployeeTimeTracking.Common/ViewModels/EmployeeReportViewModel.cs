using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Common.ViewModels
{
    public class EmployeeReportViewModel
    {
        public string FullNameEmployee { get; set; }
        public string PositionEmployee { get; set; }
        public string Name { get; set; }
        public int NumberOfHour { get; set; }
        public bool Recycling { get; set; }
        public DateTime Date { get; set; }
        public string DescriptionWork { get; set; }
    }
}
