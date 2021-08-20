using AutoMapper;
using EmployeeTimeTracking._Services.Services;
using EmployeeTimeTracking.Logic.ViewModels;
using EmployeeTimeTracking.Services.Models;
using EmployeeTimeTracking.Services.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking._Logic.Logics.Implementations
{
    public class AccountLogic : IAccountLogic
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public AccountLogic(IEmployeeService employeeService, IMapper mapper, IFileService fileService)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<bool> CreateEmployee(EmployeeViewModel model)
        {
            var types = new[] { "image/jpeg", "image/jpg", "image/png" };

            if (!types.Contains(model.Avatar.ContentType) || model == null)
            {
                return false;
            }

            model.Id = Guid.NewGuid();
            
            EmployeeModel createdModel = _mapper.Map<EmployeeModel>(model);
            await _fileService.SaveImageAsync(model.Avatar, model.Id);

            var result = await _employeeService.InsertAsync(createdModel);
            return result != null;
        }

        public async Task<bool> UpdateEmployee(EmployeeViewModel model)
        {
            var types = new[] { "image/jpeg", "image/jpg", "image/png" };

            EmployeeModel changedModel = _mapper.Map<EmployeeModel>(model);
            if(model.Avatar != null && types.Contains(model.Avatar.ContentType))
            {
                await _fileService.SaveImageAsync(model.Avatar, model.Id);
            }

            var result = await _employeeService.UpdateAsync(changedModel);
            return result != null;
        }

        public async Task<bool> DeleteEmployee(Guid id)
        {
            var result = await _employeeService.DeleteAsync(id);
            if(result != null)
            {
                _fileService.DeleteImageAsync(id);
            }

            return result != null;
        }

        public EmployeeViewModel GetEmployee(Guid id)
        {
            var employee = _employeeService.Get(id);
            EmployeeViewModel result = _mapper.Map<EmployeeViewModel>(employee.Result);
            return result;
        }
    }
}
