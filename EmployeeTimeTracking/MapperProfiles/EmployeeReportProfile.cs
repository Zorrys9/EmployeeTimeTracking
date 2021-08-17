using AutoMapper;
using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
using EmployeeTimeTracking.Data.EntityModels;
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
