﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmployeeTimeTracking.Logic.ViewModels
{
    /// <summary>
    /// Модель представления отчетов сотрудника
    /// </summary>
    public class EmployeeReportViewModel
    {
        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        [JsonProperty("ФИО сотрудника")]
        [XmlElement("ФИО_сотрудника")]
        public string FullNameEmployee { get; set; }
        /// <summary>
        /// Должность сотрудника
        /// </summary>
        [JsonProperty("Должность сотрудника")]
        [XmlElement("Должность_сотрудника")]
        public string PositionEmployee { get; set; }
        /// <summary>
        /// Количество отработанных часов
        /// </summary>
        [JsonProperty("Количество отработанных часов")]
        [XmlElement("Количество_отработанных_часов")]
        public int NumberOfHour { get; set; }
        /// <summary>
        /// Количество часов переработки
        /// </summary>
        [JsonProperty("Наличие переработки")]
        [XmlElement("Наличие_переработки")]
        public bool Recycling { get; set; }
        /// <summary>
        /// Дата создания отчета
        /// </summary>
        [JsonProperty("Дата создания отчета")]
        [XmlElement("Дата_создания_отчета")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Полное описание выполненной работы
        /// </summary>
        [JsonProperty("Полное описание выполненной работы")]
        [XmlElement("Полное_описание_выполненной_работы")]
        public string DescriptionWork { get; set; }
    }
}
