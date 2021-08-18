using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Common.ViewModels
{ 
    /// <summary>
    /// Модель представленя сотрудника
    /// </summary>
    public class EmployeeViewModel
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public Guid Id { get; set; }
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
        /// Дата рождения сотрудника
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Должность сотрудника
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Фотография сотрудника
        /// </summary>
        [IgnoreMapAttribute]
        public IFormFile Avatar { get; set; }
        /// <summary>
        /// Ссылка на изображение в системе
        /// </summary>
        public string AvatarUrl { get; set; }
    }
}
