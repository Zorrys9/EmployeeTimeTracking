using EmployeeTimeTracking.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Services.Services
{
    /// <summary>
    /// Интерфейс сервиса по работе с сотрудникам и их отчетами
    /// </summary>
    public interface IEmployeeReportService
    {
        /// <summary>
        /// Создание связи сотрудника с новым отчетом
        /// </summary>
        /// <param name="model">Модель для связи</param>
        /// <returns>Идентификатор отчета</returns>
        Task<Guid?> InsertAsync(EmployeeReportModel model);
        /// <summary>
        /// Удаление связи сотрудника с отчетом
        /// </summary>
        /// <param name="reportId">Идентификатор отчета</param>
        /// <returns>Идентификатор отчета</returns>
        Task<Guid> DeleteAsync(Guid reportId);
        /// <summary>
        /// Получение списка моделей связи по идентификатору сотрудника
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <returns>Список моделей связи</returns>
        IEnumerable<EmployeeReportModel> GetByEmployee(Guid employeeId);
        /// <summary>
        /// Получение идентификатора сотрудника по отчету
        /// </summary>
        /// <param name="reportId">Идентификатор отчета</param>
        /// <returns>Идентификатор сотрудника</returns>
        Guid GetByReport(Guid reportId);
    }
}
