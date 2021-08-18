using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Common.ViewModels
{
    /// <summary>
    /// Модель представления поиска отчетов
    /// </summary>
    public class SearchReportsViewModel
    {
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string SecondName { get; set; }
        /// <summary>
        /// Отчество сотрудника
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// От какой даты искать отчеты
        /// </summary>
        public DateTime FirstDate { get; set; }
        /// <summary>
        /// До какой даты искать отчеты
        /// </summary>
        public DateTime LastDate { get; set; }
        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public string Position { get; set; }
    }
}
