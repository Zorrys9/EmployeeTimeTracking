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
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeModel, EmployeeEntityModel>().ReverseMap();

            CreateMap<EmployeeModel, EmployeeReportViewModel>()
                .ForMember(view => view.FullNameEmployee, view => view.MapFrom(model => $"{model.FirstName} {model.SecondName} {model.LastName}"))
                .ForMember(view => view.PositionEmployee, view => view.MapFrom(model => model.Position))
                .ReverseMap();

            CreateMap<EmployeeModel, EmployeeViewModel>()
              .ForMember(view => view.AvatarUrl, view => view.MapFrom(model => model.Avatar))
              .ReverseMap();      
        }
    }
}
