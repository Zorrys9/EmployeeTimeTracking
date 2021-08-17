using EmployeeTimeTracking.Common.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Services.Services
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile file, Guid employeeId);
        void DeleteImageAsync(Guid employeeId);
        FileContentResult DownloadJson<T>(T item);
        FileContentResult DownloadXml<T>(T item);
        FileContentResult DownloadTemplateReport(Guid id);
        IEnumerable<ReportViewModel> GetReportsInXls(IFormFile file);

    }
}
