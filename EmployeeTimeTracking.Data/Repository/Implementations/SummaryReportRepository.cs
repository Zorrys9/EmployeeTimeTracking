using EmployeeTimeTracking.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository.Implementations
{
    public class SummaryReportRepository :BaseRepository<SummaryReportViewModel>, ISummaryReportRepository
    {
        public SummaryReportRepository(string connectionString)
            :base(connectionString){ }

        public SummaryReportViewModel GetSummaryReportById(Guid id)
        {
            var sqlQuery = $"SELECT COUNT(\"Id\") AS \"NumberOfReports\", SUM(\"NumberOfHour\") AS \"NumberOfHOur\", SUM(\"Recycling\") AS \"Recycling\" FROM \"Report\" as \"rep\" JOIN \"EmployeeReports\" as \"emp\" ON rep.\"Id\" = emp.\"ReportId\"  WHERE emp.\"EmployeeId\" = '{id}'";
            var result = GetEnemy(sqlQuery);
            return result;
        }

        public IEnumerable<SummaryReportViewModel> GetSummaryReports()
        {
            var sqlQuery = $"SELECT emp.\"EmployeeId\", COUNT(\"Id\") AS \"NumberOfReports\", SUM(\"NumberOfHour\") AS \"NumberOfHOur\", SUM(\"Recycling\") AS \"Recycling\" FROM \"Report\" as \"rep\" JOIN \"EmployeeReports\" as \"emp\" ON rep.\"Id\" = emp.\"ReportId\" GROUP BY emp.\"EmployeeId\"";
            var result = GetList(sqlQuery);
            return result;
        }
    }
}
