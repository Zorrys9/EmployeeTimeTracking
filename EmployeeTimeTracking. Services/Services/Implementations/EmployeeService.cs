using AutoMapper;
using EmployeeTimeTracking.Data.EntityModels;
using EmployeeTimeTracking.Data.Repository;
using EmployeeTimeTracking.Services.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTimeTracking._Services.Services.Implementations
{
    /// <summary>
    /// Сервис по работе с сотрудниками
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, ILogger logger, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<EmployeeModel> InsertAsync(EmployeeModel model)
        {
            var newModel = _mapper.Map<EmployeeEntityModel>(model);
            var result = await _employeeRepository.InsertAsync(newModel);
            if(result != null)
            {
                _logger.Information("A new employee has been added");
            }
            return _mapper.Map<EmployeeModel>(result);
        }

        public async Task<EmployeeModel> UpdateAsync(EmployeeModel model)
        {
            var newModel = _mapper.Map<EmployeeEntityModel>(model);
            var result = await _employeeRepository.UpdateAsync(newModel);
            return _mapper.Map<EmployeeModel>(result);
        }

        public async Task<EmployeeModel> DeleteAsync(Guid id)
        {
            var result = await _employeeRepository.DeleteAsync(id);
            return _mapper.Map<EmployeeModel>(result);
        }

        public async Task<IEnumerable<EmployeeModel>> GetAll()
        {
            var result = await _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<EmployeeModel>>(result);
        }

        public async Task<EmployeeModel> Get(Guid id)
        {
            var result = await _employeeRepository.Get(id);
            return _mapper.Map<EmployeeModel>(result);
        }
    }
}
