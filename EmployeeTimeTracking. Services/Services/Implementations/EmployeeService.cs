﻿using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Data.Repository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public EmployeeService(IEmployeeRepository employeeRepository, ILogger logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<Guid?> InsertAsync(EmployeeModel model)
        {
            _logger.Information("A new employee has been added");
            return await _employeeRepository.InsertAsync(model);
        }

        public async Task<Guid?> UpdateAsync(EmployeeModel model)
        {
            return await _employeeRepository.UpdateAsync(model);
        }

        public async Task<Guid?> DeleteAsync(Guid id)
        {
            return await _employeeRepository.DeleteAsync(id);
        }

        public IEnumerable<EmployeeModel> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        public EmployeeModel Get(Guid id)
        {
            return _employeeRepository.Get(id);
        }
    }
}
