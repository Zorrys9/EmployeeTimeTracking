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
    public class ReportProfile :Profile
    {
        public ReportProfile()
        {
            CreateMap<ReportModel, ReportEntityModel>().ReverseMap();

            CreateMap<ReportModel, ReportViewModel>()
                .ForMember(model => model.ReportId, model => model.MapFrom(view => view.Id)).ReverseMap();
        }
    }   
}
