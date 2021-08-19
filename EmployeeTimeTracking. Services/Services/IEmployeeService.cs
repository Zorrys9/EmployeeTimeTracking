using EmployeeTimeTracking.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking._Services.Services
{
    /// <summary>
    /// Интерфейс сервиса по работе с сотрудниками
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Создание нового сотрудника
        /// </summary>
        /// <param name="model">Модель сотрудника</param>
        /// <returns>Идентификатор сотрудника</returns>
        Task<EmployeeModel> InsertAsync(EmployeeModel model);
        /// <summary>
        /// Изменение сотрудника
        /// </summary>
        /// <param name="model">Модель сотрудника</param>
        /// <returns>Идентификатор сотрудника</returns>
        Task<EmployeeModel> UpdateAsync(EmployeeModel model);
        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Идентификатор сотрудника</returns>
        Task<EmployeeModel> DeleteAsync(Guid id);
        /// <summary>
        /// Получение списка всех сотрудников системы
        /// </summary>
        /// <returns>Список всех сотрудников системы</returns>
        Task<IEnumerable<EmployeeModel>> GetAll();
        /// <summary>
        /// Получение сотрудника по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Модель сотрудника</returns>
        Task<EmployeeModel> Get(Guid id);
    }
}
