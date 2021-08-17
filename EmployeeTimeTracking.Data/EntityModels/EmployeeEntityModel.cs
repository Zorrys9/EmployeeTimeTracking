using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.EntityModels
{
    [Table("Employee")]
    public class EmployeeEntityModel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Required]
        public string SecondName { get; set; }
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [MaxLength(30)]
        [Required]
        public string Position { get; set; }
        [Required]
        public string Avatar { get; set; }

        public IEnumerable<EmployeeReportEntityModel> EmployeeReports { get; set; }
    }
}
