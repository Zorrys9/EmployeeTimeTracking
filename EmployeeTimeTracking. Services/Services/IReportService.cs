using EmployeeTimeTracking.Common.CommonModels;
using EmployeeTimeTracking.Services.Models;
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
        Task<ReportModel> InsertAsync(ReportModel model);
        /// <summary>
        /// Изменение отчета сотрудника
        /// </summary>
        /// <param name="model">Измененный отчет</param>
        /// <returns>Идентификатор отчета</returns>
        Task<ReportModel> UpdateAsync(ReportModel model);
        /// <summary>
        /// Удаление отчета сотрудника
        /// </summary>
        /// <param name="model">Удаленный отчет</param>
        /// <returns>Идентификатор отчета</returns>
        Task<ReportModel> DeleteAsync(Guid Id);
        /// <summary>
        /// Вывод отчетов всех сотрудников на страницу
        /// </summary>
        /// <returns>Список всех отчетов в системе</returns>
        Task<IEnumerable<ReportModel>> GetAllForPage(int pageSize, int currentPage);
        /// <summary>
        /// Вывод всех отчетов всех сотрудников
        /// </summary>
        /// <returns>Список всех отчетов в системе</returns>
        Task<IEnumerable<ReportModel>> GetAll();
        /// <summary>
        /// Получение отчета по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор отчета</param>
        /// <returns>Модель отчета</returns>
        Task<ReportModel> Get(Guid id);
        /// <summary>
        /// Создание краткого отчета по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор отчета</param>
        /// <returns>Модель представление краткого отчета</returns>
       Task<SummaryReportModel> SummaryReport(Guid id);
        /// <summary>
        /// Получение всех кратких отчетов
        /// </summary>
        /// <returns>Список моделей представления кратких отчетов</returns>
       Task<IEnumerable<SummaryReportModel>> SummaryReports();
        /// <summary>
        /// Получение всех отчетов с возможностью фильтрации
        /// </summary>
        /// <param name="model">Модель представления фильтра поиска</param>
        /// <returns>Список моделей представления кратких отчетов</returns>
       Task<ICollection<EmployeeReportModel>> SearchReports(SearchReportModel model, int pageSize, int currentPage);
       /// <summary>
       /// Получение количества всех отчетов в БД
       /// </summary>
       /// <returns>Количество созданных в БД отчетов</returns>
        Task<int> CountAll();
        /// <summary>
        /// Получение количества отчетов в БД, подходящих под условия
        /// </summary>
        /// <param name="model">Модель с условиями поиска отчетов</param>
        /// <returns>Количество найденных отчетов</returns>
        Task<int> CountFound(SearchReportModel model);
    }
}
