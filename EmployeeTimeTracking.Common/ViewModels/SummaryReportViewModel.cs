using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmployeeTimeTracking.Common.ViewModels
{
    /// <summary>
    /// Модель представления общего отчета по сотруднику
    /// </summary>
    public class SummaryReportViewModel
    {
        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        [JsonProperty("ФИО сотрудника")]
        [XmlElement("ФИО сотрудника")]
        public string FullName { get; set; }
        /// <summary>
        /// Должность сотрудника
        /// </summary>
        [JsonProperty("Должность сотрудника")]
        [XmlElement("Должность сотрудника")]
        public string Position { get; set; }
        /// <summary>
        /// Общее количество отработанных часов
        /// </summary>
        [JsonProperty("Общее количество отработанных часов")]
        [XmlElement("Общее количество отработанных часов")]
        public int NumberOfHour { get; set; }
        /// <summary>
        /// Общее количество часов переработки
        /// </summary>
        [JsonProperty("Общее количество часов переработки")]
        [XmlElement("Общее количество часов переработки")]
        public int Recycling { get; set; }
        /// <summary>
        /// Количество написанных отчетов
        /// </summary>
        [JsonProperty("Количество написанных отчетов")]
        [XmlElement("Количество написанных отчетов")]
        public int NumberOfReports { get; set; }
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public Guid EmployeeId { get; set; }
    }
}
