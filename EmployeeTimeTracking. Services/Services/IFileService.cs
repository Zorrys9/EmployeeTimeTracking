using EmployeeTimeTracking.Common.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Services.Services
{
   /// <summary>
   /// Интерфейс сервиса по работе с файлами в системе
   /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// сохранение изображения сотрудника
        /// </summary>
        /// <param name="file">Файл с изображением сотрудника</param>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <returns>Ссылка на изображение в системе</returns>
        Task SaveImageAsync(IFormFile file, Guid employeeId);
        /// <summary>
        /// Удаление изображения сотрудника
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        void DeleteImageAsync(Guid employeeId);
        /// <summary>
        /// Преобразование списка объектов в json и возврат json-файла
        /// </summary>
        /// <typeparam name="T">Тип объектов для сериализации</typeparam>
        /// <param name="item">Список объектов для сериализации</param>
        /// <returns>Json-файл со список сериализованных объектов</returns>
        FileContentResult DownloadJson<T>(T item);
        /// <summary>
        /// Преобразование списка объектов в json и возврат xml-файла
        /// </summary>
        /// <typeparam name="T">Тип объектов для сериализации</typeparam>
        /// <param name="item">Список объектов для сериализации</param>
        /// <returns>Xml-файл со список сериализованных объектов</returns>
        FileContentResult DownloadXml<T>(T item);
        /// <summary>
        /// Запрос на скачивание сотрудником xls-шаблона для заполнения отчетов в Excel
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <returns>Xls-файл с шаблоном заполнения отчетов</returns>
        FileContentResult DownloadTemplateReport(Guid id);
        /// <summary>
        /// Считывание отчетов из xls-файла (если он не заполнен по шаблону - работать не будет)
        /// </summary>
        /// <param name="file">Xls-файл с отчетами</param>
        /// <returns>Список моделей представления отчетов полученных из xls-файла</returns>
        IEnumerable<ReportViewModel> GetReportsFromXls(IFormFile file);
    }
}
