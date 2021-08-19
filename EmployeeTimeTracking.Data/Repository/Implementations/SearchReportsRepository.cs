using EmployeeTimeTracking.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Data.Repository.Implementations
{
    public class SearchReportsRepository : BaseRepository<EmployeeReportViewModel>,ISearchReportsRepository
    {
        public SearchReportsRepository(string connectionString)
            :base(connectionString)   {}


        public async Task<ICollection<EmployeeReportViewModel>> SearchReports(SearchReportsViewModel model, PageInfoViewModel pageInfo)
        {
            var sqlQuery = $"SELECT (emp.\"FirstName\" || ' ' || emp.\"SecondName\" || ' ' || emp.\"LastName\") AS \"FullNameEmployee\", emp.\"Position\" AS \"PositionEmployee\", rep.\"Date\", rep.\"NumberOfHour\", rep.\"Recycling\", rep.\"DescriptionWork\" FROM \"EmployeeReports\" AS empRep JOIN \"Employee\" AS \"emp\" ON emp.\"Id\" = empRep.\"EmployeeId\" JOIN \"Report\" AS \"rep\" ON rep.\"Id\" = empRep.\"ReportId\"";
            var whereComponent = $" WHERE (emp.\"FirstName\" LIKE '%{model.FirstName}%') AND (emp.\"SecondName\" LIKE '%{model.SecondName}%') AND (emp.\"LastName\" LIKE '%{model.LastName}%') AND (emp.\"Position\" LIKE '%{model.Position}%') AND (rep.\"Date\" >= '{model.FirstDate}') AND (rep.\"Date\" <= '{model.LastDate}')";
            var pagination = $" LIMIT {pageInfo.PageSize} OFFSET {(pageInfo.CurrentPage - 1) * pageInfo.PageSize}";

            sqlQuery += whereComponent + pagination;
            var result = await GetListAsync(sqlQuery);
            return result.ToList();
        }

        public async Task<int> Count(SearchReportsViewModel model)
        {
            var sqlQuery = $"SELECT COUNT(*) FROM \"EmployeeReports\" AS empRep JOIN \"Employee\" AS \"emp\" ON emp.\"Id\" = empRep.\"EmployeeId\" JOIN \"Report\" AS \"rep\" ON rep.\"Id\" = empRep.\"ReportId\"";
            var whereComponent = $" WHERE (emp.\"FirstName\" LIKE '%{model.FirstName}%') AND (emp.\"SecondName\" LIKE '%{model.SecondName}%') AND (emp.\"LastName\" LIKE '%{model.LastName}%') AND (emp.\"Position\" LIKE '%{model.Position}%') AND (rep.\"Date\" >= '{model.FirstDate}') AND (rep.\"Date\" <= '{model.LastDate}')";

            sqlQuery += whereComponent;
            var result = await GetColumnAsync(sqlQuery);
            return Convert.ToInt32(result);
        }
    }
}
