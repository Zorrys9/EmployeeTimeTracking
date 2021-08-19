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
            var result = await _reportService.DeleteAsync(resultDelete.ReportId);

            return result != null;
        }

        public async Task<bool> ReportInsertAsync(ReportViewModel model)
        {
            var reportModel = _mapper.Map<ReportModel>(model);
            var report = await _reportService.InsertAsync(reportModel);
            var employeeReportModel = _mapper.Map<EmployeeReportModel>(model);
            employeeReportModel.ReportId =  report.Id;

            var result = await _employeeReportService.InsertAsync(employeeReportModel);
            return result != null;
        }

        public async Task<PaginationViewModel<EmployeeReportViewModel>> GetAllReportsInPage(PageInfoViewModel pageInfo)
        {
            pageInfo.CountItems = await _reportService.CountAll();
            pageInfo.CalculateTotalPage();

            var reportsList = await _reportService.GetAllForPage(pageInfo);
            PaginationViewModel<EmployeeReportViewModel> result = new PaginationViewModel<EmployeeReportViewModel>();

            foreach(var report in reportsList)
            {
                var employeeId = await _employeeReportService.GetByReport(report.Id);
                EmployeeReportViewModel viewModel = _mapper.Map<EmployeeReportViewModel>(report);
                var employee = await _employeeService.Get(employeeId);
                viewModel.FullNameEmployee =  _mapper.Map<EmployeeReportViewModel>(employee).FullNameEmployee;
                viewModel.PositionEmployee = employee.Position;

                result.List.Add(viewModel);
            }
            return result;
        }

        public async Task<ICollection<EmployeeReportViewModel>> GetAllReports()
        {
            var reportsList = await _reportService.GetAll();
            ICollection<EmployeeReportViewModel> result = new List<EmployeeReportViewModel>();

            foreach (var report in reportsList)
            {
                var employeeId = await _employeeReportService.GetByReport(report.Id);
                EmployeeReportViewModel viewModel = _mapper.Map<EmployeeReportViewModel>(report);
                var employee = await _employeeService.Get(employeeId);
                viewModel.FullNameEmployee = _mapper.Map<EmployeeReportViewModel>(employee).FullNameEmployee;
                viewModel.PositionEmployee = employee.Position;

                result.Add(viewModel);
            }
            return result;
        }

        public async Task<PaginationViewModel<EmployeeReportViewModel>> SearchReports([FromForm]SearchReportsViewModel model, PageInfoViewModel pageInfo)
        {
            pageInfo.CountItems = await _reportService.CountFound(model);
            pageInfo.CalculateTotalPage();
            var reports = await _reportService.SearchReports(model, pageInfo);

            PaginationViewModel<EmployeeReportViewModel> result = new();

            result.PageInfo = pageInfo;
            result.List = reports;

            return result;
        }

        public async Task<PaginationViewModel<EmployeeReportViewModel>> GetReportsByEmployeeForPage(Guid employeeId, PageInfoViewModel pageInfo)
        {
            pageInfo.CountItems = await _employeeReportService.CountByEmployee(employeeId);
            pageInfo.CalculateTotalPage();

            var employeeReportsList = await _employeeReportService.GetByEmployeeForPage(employeeId, pageInfo);
            PaginationViewModel<EmployeeReportViewModel> pagination = new();
            pagination.PageInfo = pageInfo;
            pagination.List = new List<EmployeeReportViewModel>();
            
            foreach(var item in employeeReportsList)
            {
                var employee = await _employeeService.Get(item.EmployeeId);
                var report = await _reportService.GetById(item.ReportId);
                EmployeeReportViewModel viewModel = _mapper.Map<EmployeeReportViewModel>(report);
                viewModel.FullNameEmployee = _mapper.Map<EmployeeReportViewModel>(employee).FullNameEmployee;
                viewModel.PositionEmployee = employee.Position;
                pagination.List.Add(viewModel);
            }
            return pagination;
        }

        public async Task<ICollection<EmployeeReportViewModel>> GetReportsByEmployee(Guid employeeId)
        {

            var employeeReportsList = await _employeeReportService.GetByEmployee(employeeId);
            ICollection<EmployeeReportViewModel> result = new List<EmployeeReportViewModel>();

            foreach (var item in employeeReportsList)
            {
                var employee = await _employeeService.Get(item.EmployeeId);
                var report = await _reportService.GetById(item.ReportId);
                EmployeeReportViewModel viewModel = _mapper.Map<EmployeeReportViewModel>(report);
                viewModel.FullNameEmployee = _mapper.Map<EmployeeReportViewModel>(employee).FullNameEmployee;
                viewModel.PositionEmployee = employee.Position;
                result.Add(viewModel);
            }
            return result;
        }

        public async Task<FileContentResult> DetailReportForAllInJson()
        {
            var reports = await GetAllReports();
            return _fileService.DownloadJson(reports);
        }

        public async Task<FileContentResult> DetailReportForAllInXml()
        {
            var reports = await GetAllReports();
            return _fileService.DownloadXml(reports);
        }

        public async Task<FileContentResult> DetailReportForEmployeeInJson(Guid employeeId)
        {
            var reports = await GetReportsByEmployee(employeeId);
            return _fileService.DownloadJson(reports);
        }
        
        public async Task<FileContentResult> DetailReportForEmployeeInXml(Guid employeeId)
        {
            var reports = await GetReportsByEmployee(employeeId);
            return _fileService.DownloadXml(reports);
        }
        public async Task<FileContentResult> SummaryReportForEmployeeInJson(Guid employeeId)
        {
            var report = await _reportService.SummaryReportById(employeeId);
            var employee = await _employeeService.Get(employeeId);
            report.FullName = $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
            report.Position = employee.Position;
            return _fileService.DownloadJson(report);
        }

        public async Task<FileContentResult> SummaryReportForEmployeeInXml(Guid employeeId)
        {
            var report = await _reportService.SummaryReportById(employeeId);
            var employee = await _employeeService.Get(employeeId);
            report.FullName = $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
            report.Position = employee.Position;
            return _fileService.DownloadXml(report);
        }
        public async Task<FileContentResult> SummaryReportsInJson()
        {
            var reports = await _reportService.SummaryReports();
            foreach (var report in reports)
            {
                var employee = await  _employeeService.Get(report.EmployeeId);
                report.FullName = $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
                report.Position = employee.Position;
            }
            return _fileService.DownloadJson(reports);
        }

        public async Task<FileContentResult> SummaryReportsInXml()
        {
            var reports = await _reportService.SummaryReports();
            foreach (var report in reports)
            {
                var employee = await _employeeService.Get(report.EmployeeId);
                report.FullName = $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
                report.Position = employee.Position;
            }
            return _fileService.DownloadXml(reports);
        }

        public async Task<bool> SetReportsFromXls(IFormFile file)
        {
            var reports = _fileService.GetReportsFromXls(file);

            if(reports == null || file == null)
            {
                return false;
            }

            foreach(var report in reports)
            {
                var newReport = _mapper.Map<ReportModel>(report);
                var reportModel = await _reportService.InsertAsync(newReport);
                EmployeeReportModel employeeReport = new EmployeeReportModel()
                {
                    EmployeeId = report.EmployeeId,
                    ReportId = reportModel.Id
                };
                await _employeeReportService.InsertAsync(employeeReport);
            }
            return true;
        }
    }
}
