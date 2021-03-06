using AutoMapper;
using EmployeeTimeTracking._Services.Services;
using EmployeeTimeTracking.Common.CommonModels;
using EmployeeTimeTracking.Logic.ViewModels;
using EmployeeTimeTracking.Services.Models;
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
        private readonly IEmployeeReportService _employeeReportService;
        private readonly IEmployeeService _employeeService;
        private readonly IReportService _reportService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        public TrackingLogic(IReportService reportService, IEmployeeReportService employeeReportService,
            IMapper mapper, IEmployeeService employeeService, IFileService fileService)
        {
            _employeeReportService = employeeReportService;
            _employeeService = employeeService;
            _reportService = reportService;
            _fileService = fileService;
            _mapper = mapper;
        }
        public async Task<bool> DeleteReportAsync(Guid reportId)
        {
            var resultDelete = await _employeeReportService.DeleteAsync(reportId);
            var result = await _reportService.DeleteAsync(resultDelete.ReportId);
            return result != null;
        }

        public async Task<bool> InsertReportAsync(ReportViewModel model)
        {
            var reportModel = _mapper.Map<ReportModel>(model);
            var report = await _reportService.InsertAsync(reportModel);
            var employeeReportModel = _mapper.Map<EmployeeReportModel>(model);
            employeeReportModel.ReportId =  report.Id;
            var result = await _employeeReportService.InsertAsync(employeeReportModel);
            return result != null;
        }

        public async Task<PaginationViewModel<EmployeeReportViewModel>> GetReportsForPage(PageInfoViewModel pageInfo)
        {
            pageInfo.CountItems = await _reportService.CountAll();
            pageInfo.CalculateTotalPage();

            var reportsList = await _reportService.GetAllForPage(pageInfo.PageSize, pageInfo.CurrentPage);
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

        public async Task<PaginationViewModel<EmployeeReportViewModel>> SearchReports(SearchReportModel model, PageInfoViewModel pageInfo)
        {
            pageInfo.CountItems = await _reportService.CountFound(model);
            pageInfo.CalculateTotalPage();
            var reports = await _reportService.SearchReports(model, pageInfo.PageSize, pageInfo.CurrentPage);

            PaginationViewModel<EmployeeReportViewModel> result = new();

            result.PageInfo = pageInfo;
            result.List = _mapper.Map<ICollection<EmployeeReportViewModel>>(reports);
            return result;
        }

        public async Task<PaginationViewModel<EmployeeReportViewModel>> GetReportsEmployeeForPage(Guid employeeId, PageInfoViewModel pageInfo)
        {
            pageInfo.CountItems = await _employeeReportService.CountByEmployee(employeeId);
            pageInfo.CalculateTotalPage();

            var employeeReportsList = await _employeeReportService.GetByEmployeeForPage(employeeId, pageInfo.PageSize, pageInfo.CurrentPage);
            PaginationViewModel<EmployeeReportViewModel> pagination = new();
            pagination.PageInfo = pageInfo;
            pagination.List = new List<EmployeeReportViewModel>();
            
            foreach(var item in employeeReportsList)
            {
                var employee = await _employeeService.Get(item.EmployeeId);
                var report = await _reportService.Get(item.ReportId);
                EmployeeReportViewModel viewModel = _mapper.Map<EmployeeReportViewModel>(report);
                viewModel.FullNameEmployee = _mapper.Map<EmployeeReportViewModel>(employee).FullNameEmployee;
                viewModel.PositionEmployee = employee.Position;
                pagination.List.Add(viewModel);
            }
            return pagination;
        }

        public async Task<ICollection<EmployeeReportViewModel>> GetReportsEmployee(Guid employeeId)
        {
            var employeeReportsList = await _employeeReportService.GetByEmployee(employeeId);
            ICollection<EmployeeReportViewModel> result = new List<EmployeeReportViewModel>();

            foreach (var item in employeeReportsList)
            {
                var employee = await _employeeService.Get(item.EmployeeId);
                var report = await _reportService.Get(item.ReportId);
                EmployeeReportViewModel viewModel = _mapper.Map<EmployeeReportViewModel>(report);
                viewModel.FullNameEmployee = _mapper.Map<EmployeeReportViewModel>(employee).FullNameEmployee;
                viewModel.PositionEmployee = employee.Position;
                result.Add(viewModel);
            }
            return result;
        }

        public async Task<FileContentResult> DetailReportsJson()
        {
            var reports = await GetAllReports();
            return _fileService.GetJson(reports);
        }

        public async Task<FileContentResult> DetailReportsXml()
        {
            var reports = await GetAllReports();
            return _fileService.GetXml(reports);
        }

        public async Task<FileContentResult> DetailReportsEmployeeJson(Guid employeeId)
        {
            var reports = await GetReportsEmployee(employeeId);
            return _fileService.GetJson(reports);
        }
        
        public async Task<FileContentResult> DetailReportsEmployeeXml(Guid employeeId)
        {
            var reports = await GetReportsEmployee(employeeId);
            return _fileService.GetXml(reports);
        }
        public async Task<FileContentResult> SummaryReportsEmployeeJson(Guid employeeId)
        {
            var report = await _reportService.SummaryReport(employeeId);
            var employee = await _employeeService.Get(employeeId);
            report.FullName = $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
            report.Position = employee.Position;
            return _fileService.GetJson(report);
        }

        public async Task<FileContentResult> SummaryReportsEmployeeXml(Guid employeeId)
        {
            var report = await _reportService.SummaryReport(employeeId);
            var employee = await _employeeService.Get(employeeId);
            report.FullName = $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
            report.Position = employee.Position;
            return _fileService.GetXml(report);
        }
        public async Task<FileContentResult> SummaryReportsJson()
        {
            var reports = await _reportService.SummaryReports();
            foreach (var report in reports)
            {
                var employee = await  _employeeService.Get(report.EmployeeId);
                report.FullName = $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
                report.Position = employee.Position;
            }
            return _fileService.GetJson(reports);
        }

        public async Task<FileContentResult> SummaryReportsXml()
        {
            var reports = await _reportService.SummaryReports();
            foreach (var report in reports)
            {
                var employee = await _employeeService.Get(report.EmployeeId);
                report.FullName = $"{employee.FirstName} {employee.SecondName} {employee.LastName}";
                report.Position = employee.Position;
            }
            return _fileService.GetXml(reports);
        }

        public async Task<bool> SetReports(IFormFile file)
        {
            var reports = _fileService.GetReportsFromFile(file);

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
