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
   /// Интерфейс сервиса по работе с отчетами сотрудников
   /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Создание нового отчета сотрудника
        /// </summary>
        /// <param name="model">Модель отчета сотрудника</param>
        /// <returns>Идентификатор отчета</returns>
        Task<Guid?> InsertAsync(ReportModel model);
        /// <summary>
        /// Изменение отчета сотрудника
        /// </summary>
        /// <param name="model">Измененный отчет</param>
        /// <returns>Идентификатор отчета</returns>
        Task<Guid?> UpdateAsync(ReportModel model);
        /// <summary>
        /// Удаление отчета сотрудника
        /// </summary>
        /// <param name="model">Удаленный отчет</param>
        /// <returns>Идентификатор отчета</returns>
        Task<Guid?> DeleteAsync(Guid Id);
        /// <summary>
        /// Вывод всех отчетов всех сотрудников
        /// </summary>
        /// <returns>Список всех отчетов в системе</returns>
        IEnumerable<ReportModel> GetAll();
        /// <summary>
        /// Получение отчета по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор отчета</param>
        /// <returns>Модель отчета</returns>
        ReportModel GetById(Guid id);
        /// <summary>
        /// Создание краткого отчета по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор отчета</param>
        /// <returns>Модель представление краткого отчета</returns>
        SummaryReportViewModel SummaryReportById(Guid id);
        /// <summary>
        /// Получение всех кратких отчетов
        /// </summary>
        /// <returns>Список моделей представления кратких отчетов</returns>
        IEnumerable<SummaryReportViewModel> SummaryReports();
        /// <summary>
        /// Получение всех отчетов с возможностью фильтрации
        /// </summary>
        /// <param name="model">Модель представления фильтра поиска</param>
        /// <returns>Список моделей представления кратких отчетов</returns>
        IEnumerable<EmployeeReportViewModel> SearchReports(SearchReportsViewModel model);
    }
}
