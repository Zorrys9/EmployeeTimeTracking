using AutoMapper;
using EmployeeTimeTracking._Services.Services;
using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
using EmployeeTimeTracking.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeTimeTracking._Logic.Logics.Implementations
{
    public class TrackingLogic : ITrackingLogic
    {
        private readonly IReportService _reportService;
        private readonly IEmployeeReportService _employeeReportService;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public TrackingLogic(IReportService reportService, IEmployeeReportService employeeReportService, IMapper mapper, IEmployeeService employeeService, IFileService fileService)
        {
            _reportService = reportService;
            _employeeReportService = employeeReportService;
            _mapper = mapper;
            _employeeService = employeeService;
            _fileService = fileService;
        }
        public async Task<bool> ReportDeleteAsync(Guid reportId)
        {
            var resultDelete = await _employeeReportService.DeleteAsync(reportId);
            var result = await _reportService.DeleteAsync(resultDelete);
            
            if(result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ReportInsertAsync(ReportViewModel model)
        {
            var reportModel = _mapper.Map<ReportModel>(model);
            var reportId = _reportService.InsertAsync(reportModel);
            var employeeReportModel = _mapper.Map<EmployeeReportModel>(model);
            employeeReportModel.ReportId = (Guid)await reportId;

            var result = await _employeeReportService.InsertAsync(employeeReportModel);
            if (string.IsNullOrEmpty(result.ToString()))
            {
                return false;
            }
            return true;
        }

        public IEnumerable<EmployeeReportViewModel> GetAllReports()
        {
            var reportsList = _reportService.GetAll();
            IList<EmployeeReportViewModel> result = new List<EmployeeReportViewModel>();

            foreach(var report in reportsList)
            {
                var employeeId = _employeeReportService.GetByReport(report.Id);
                EmployeeReportViewModel viewModel = _mapper.Map<EmployeeReportViewModel>(report);
                var employee = _employeeService.GetEmployee(employeeId);
                viewModel.FullNameEmployee =  _mapper.Map<EmployeeReportViewModel>(employee).FullNameEmployee;
                viewModel.PositionEmployee = employee.Position;

                result.Add(viewModel);
            }
            return result;
        }

        public IEnumerable<EmployeeReportViewModel> GetReportsForEmployee(Guid employeeId)
        {
            var employeeReportsList = _employeeReportService.GetByEmployee(employeeId);
            IList<EmployeeReportViewModel> result = new List<EmployeeReportViewModel>();
            
            foreach(var item in employeeReportsList)
            {
                var employee = _employeeService.GetEmployee(item.EmployeeId);
                var report = _reportService.GetReportById(item.ReportId);
                EmployeeReportViewModel viewModel = _mapper.Map<EmployeeReportViewModel>(report);
                viewModel.FullNameEmployee = _mapper.Map<EmployeeReportViewModel>(employee).FullNameEmployee;
                viewModel.PositionEmployee = employee.Position;
                result.Add(viewModel);
            }
            return result;
        }

        public FileContentResult DetailReportForAllInJson()
        {
            var reports = GetAllReports();
            return _fileService.DownloadJson(reports);
        }

        public FileContentResult DetailReportForAllInXml()
        {
            var reports = GetAllReports();
            return _fileService.DownloadXml(reports);
        }

        public FileContentResult DetailReportForEmployeeInJson(Guid employeeId)
        {
            var reports = GetReportsForEmployee(employeeId);
            return _fileService.DownloadJson(reports);
        }
        
        public FileContentResult DetailReportForEmployeeInXml(Guid employeeId)
        {
            var reports = GetReportsForEmployee(employeeId);
            return _fileService.DownloadXml(reports);
        }
        public FileContentResult SummaryReportForEmployeeInJson(Guid employeeId)
        {
            var report = _reportService.SummaryReportById(employeeId);
            var employee = _employeeService.GetEmployee(employeeId);
            report.FullName = $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
            report.Position = employee.Position;
            return _fileService.DownloadJson(report);
        }

        public FileContentResult SummaryReportForEmployeeInXml(Guid employeeId)
        {
            var report = _reportService.SummaryReportById(employeeId);
            var employee = _employeeService.GetEmployee(employeeId);
            report.FullName = $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
            report.Position = employee.Position;
            return _fileService.DownloadXml(report);
        }
        public FileContentResult SummaryReportsInJson()
        {
            var reports = _reportService.SummaryReports();
            foreach (var report in reports)
            {
                var employee = _employeeService.GetEmployee(report.EmployeeId);
                report.FullName = $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
                report.Position = employee.Position;
            }
            return _fileService.DownloadJson(reports);
        }

        public FileContentResult SummaryReportsInXml()
        {
            var reports = _reportService.SummaryReports();
            foreach (var report in reports)
            {
                var employee = _employeeService.GetEmployee(report.EmployeeId);
                report.FullName = $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
                report.Position = employee.Position;
            }
            return _fileService.DownloadXml(reports);
        }

        public async Task<bool> SetReportsFromXls(IFormFile file)
        {
            var reports = _fileService.GetReportsInXls(file);

            if(reports == null || file == null)
            {
                return false;
            }

            foreach(var report in reports)
            {
                var newReport = _mapper.Map<ReportModel>(report);
                var reportId = await _reportService.InsertAsync(newReport);
                EmployeeReportModel employeeReport = new EmployeeReportModel()
                {
                    EmployeeId = report.EmployeeId,
                    ReportId = reportId.Value
                };
                await _employeeReportService.InsertAsync(employeeReport);
            }
            return true;
        }
    }
}
