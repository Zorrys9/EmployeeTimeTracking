using AutoMapper;
using EmployeeTimeTracking.Data.EntityModels;
using EmployeeTimeTracking.Logic.ViewModels;
using EmployeeTimeTracking.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.MapperProfiles
{
    public class ReportProfile :Profile
    {
        public ReportProfile()
        {
            CreateMap<ReportModel, ReportEntityModel>().ReverseMap();

            CreateMap<ReportModel, ReportViewModel>()
                .ForMember(view => view.ReportId, view => view.MapFrom(model => model.Id)).ReverseMap();

            CreateMap<ReportModel, EmployeeReportViewModel>()
                .ForMember(view => view.Recycling, view => view.MapFrom(model => model.Recycling > 0))
                .ForMember(view => view.Date, view => view.MapFrom(model => model.Date.ToShortDateString()))
                .ReverseMap();

            CreateMap<SummaryReportModel, SummaryReportEntityModel>().ReverseMap();
            CreateMap<SummaryReportModel, SummaryReportViewModel>().ReverseMap();

            CreateMap<EmployeeReportModel, DetailReportEntityModel>().ReverseMap();
        }
    }   
}
