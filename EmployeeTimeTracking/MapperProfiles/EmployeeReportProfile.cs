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
    public class EmployeeReportProfile:Profile
    {
        public EmployeeReportProfile()
        {
            CreateMap<EmployeeReportModel, EmployeeReportEntityModel>().ReverseMap();
            CreateMap<EmployeeReportModel, ReportViewModel>().ReverseMap();
        }
    }
}
