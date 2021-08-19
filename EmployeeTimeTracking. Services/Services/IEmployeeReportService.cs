using EmployeeTimeTracking.Common.Models;
using EmployeeTimeTracking.Common.ViewModels;
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
        Task<EmployeeReportModel> InsertAsync(EmployeeReportModel model);
        /// <summary>
        /// Удаление связи сотрудника с отчетом
        /// </summary>
        /// <param name="reportId">Идентификатор отчета</param>
        /// <returns>Идентификатор отчета</returns>
        Task<EmployeeReportModel> DeleteAsync(Guid reportId);
        /// <summary>
        /// Получение списка моделей связи по идентификатору сотрудника
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <returns>Список моделей связи</returns>
        Task<IEnumerable<EmployeeReportModel>> GetByEmployee(Guid employeeId);
        /// <summary>
        /// Получение списка моделей связи по идентификатору сотрудника в количестве, указанном в информации о странице
        /// </summary>
        /// <param name="employeeId">Идентификатор пользователя</param>
        /// <param name="pageInfo">Информация о текущей странице</param>
        /// <returns>Список моделей связи</returns>
        Task<IEnumerable<EmployeeReportModel>> GetByEmployeeForPage(Guid employeeId, PageInfoViewModel pageInfo);
        /// <summary>
        /// Получение количества записей в таблице связей по идентификатору сотрудника
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <returns>Количество записей</returns>
        Task<int> CountByEmployee(Guid employeeId);
        /// <summary>
        /// Получение идентификатора сотрудника по отчету
        /// </summary>
        /// <param name="reportId">Идентификатор отчета</param>
        /// <returns>Идентификатор сотрудника</returns>
        Task<Guid> GetByReport(Guid reportId);
    }
}
