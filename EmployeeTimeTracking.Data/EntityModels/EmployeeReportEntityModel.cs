using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.EntityModels
{
    [Table("EmployeeReports")]
    public class EmployeeReportEntityModel
    {
        [Key]
        [Required]
        public Guid ReportId { get; set; }
        [Required]
        public Guid EmployeeId { get; set; }

        public ReportEntityModel Report { get; set; }

        public EmployeeEntityModel Employee { get; set; }
    }
}
