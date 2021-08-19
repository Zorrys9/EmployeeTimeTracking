using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Common.ViewModels
{
    /// <summary>
    /// Модель представления отчета
    /// </summary>
    public class ReportViewModel
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        [Required(ErrorMessage ="Не указан идентификатор сотрудника")]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Идентификатор отчета
        /// </summary>
        [Required(ErrorMessage = "Не указан идентификатор отчета")]
        public Guid ReportId { get; set; }
        /// <summary>
        /// Дата создания отчета
        /// </summary>
        [Required(ErrorMessage ="Не указана дата создания отчета")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Количество отработанных часов за день
        /// </summary>
        [Required(ErrorMessage ="Количество отработанных часов не указано")]
        public int NumberOfHour { get; set; }
        /// <summary>
        /// Количество часов переработки
        /// </summary>
        public int Recycling { get; set; }
        /// <summary>
        /// Причина переработки
        /// </summary>
        [MaxLength(50,  ErrorMessage ="Причина переработки не должна превышать 50 символов")]
        public string ReasonForRecycling { get; set; }
        /// <summary>
        /// Подробное описание выполненной за день работы
        /// </summary>
        [Required(ErrorMessage ="Подробное описание работы не указано")]
        [MaxLength(100, ErrorMessage ="Подробное описание работы не должно превышать 100 символов")]
        public string DescriptionWork { get; set; }

    }
}
