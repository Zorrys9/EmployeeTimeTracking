using EmployeeTimeTracking.Common.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmployeeTimeTracking.Services.Services.Implementations
{
    /// <summary>
    /// Сервис по работе с файлами
    /// </summary>
    public class FileService : IFileService
    {
        public async Task SaveImageAsync(IFormFile file, Guid employeeId)
        {
            var fileName = employeeId + file.ContentType.Replace("image/", ".");
            var fileStorage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\AccountImages");
            var filePath = Path.Combine(fileStorage, fileName);

            if (!string.IsNullOrEmpty(FindFile(employeeId)))
            {
                DeleteImageAsync(employeeId);
            }

            if (!Directory.Exists(fileStorage))
            {
                Directory.CreateDirectory(fileStorage);
            }
            using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public void DeleteImageAsync(Guid employeeId)
        {
            var image = FindFile(employeeId);
            File.Delete(image);
        }

        public FileContentResult DownloadJson<T>(T item)
        {
            var json = JsonConvert.SerializeObject(item);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\response.json");
            File.WriteAllText(filePath, json);

            FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(filePath), "application/json")
            {
                FileDownloadName = $"response_{DateTime.Now.ToShortDateString()}.json"
            };

            File.Delete(filePath);
            return result;
        }

        public FileContentResult DownloadXml<T>(T item)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(item.GetType());
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\response.xml");
            
            using(Stream stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(stream, item);
            }

            FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(filePath), "application/xml")
            {
                FileDownloadName = $"response_{DateTime.Now.ToShortDateString()}.xml"
            };

            File.Delete(filePath);
            return result;
        }

        public FileContentResult DownloadTemplateReport(Guid id)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\templateReport.xlsx");
            FileContentResult result = new FileContentResult(System.IO.File.ReadAllBytes(filePath), "application/vnd.ms-excel")
            {
                FileDownloadName = $"{id}.xlsx"
            };
            return result;
        }

        public IEnumerable<ReportViewModel> GetReportsFromXls(IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage(file.OpenReadStream());
            var sheets = package.Workbook.Worksheets[0];
            var fileName = file.FileName;
            var employeeId = fileName.Substring(0, fileName.LastIndexOf('.'));
            var i = 2;
            List<ReportViewModel> result = new List<ReportViewModel>();
            while (sheets.Cells[i, 2].Value != null)
            {
                ReportViewModel viewModel = new ReportViewModel();
                viewModel.Date = (DateTime)sheets.Cells[i, 2].Value;
                viewModel.NumberOfHour = Convert.ToInt32(sheets.Cells[i, 3].Value);
                viewModel.Recycling = Convert.ToInt32(sheets.Cells[i, 4].Value);
                viewModel.ReasonForRecycling = sheets.Cells[i, 5].Value?.ToString();
                viewModel.DescriptionWork = sheets.Cells[i, 6].Value.ToString();
                viewModel.EmployeeId = Guid.Parse(employeeId);
                result.Add(viewModel);
                i++;
            }
            return result;
        }

        /// <summary>
        /// Ищет изображение сотрудника по его идентификатору
        /// </summary>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <returns>Ссылка на изображение сотрудника</returns>
        private string FindFile(Guid employeeId)
        {
            var fileName = employeeId.ToString();
            var fileStorage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\AccountImages");
            var files = Directory.GetFiles(fileStorage).ToList();

            var image = files.Find(file => file.Contains(fileName));

            return image;
        }
    }
}

