using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Common.ViewModels
{
    /// <summary>
    /// Модель представления информации о странице
    /// </summary>
    public class PageInfoViewModel
    {
        /// <summary>
        /// Общее количество страниц
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Текущая страница
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Общее количество объектов
        /// </summary>
        public int CountItems { get; set; }
        /// <summary>
        /// Количество выводимых объектов
        /// </summary>
        public int PageSize { get; set; }
        public PageInfoViewModel(int pageNumber, int countItems, int pageSize)
        {
            CurrentPage = pageNumber;
            CountItems = countItems;
            PageSize = pageSize;
        }
        /// <summary>
        /// Проверка есть ли предыдущая страница
        /// </summary>
        public bool PrevPageExist 
        {
            get { return CurrentPage > 1 && CurrentPage <= TotalPages + 1; }
        }

        /// <summary>
        /// Проверка есть ли следующая страница
        /// </summary>
        public bool NextPageExist
        {
            get { return CurrentPage < TotalPages; }
        }

        public void CalculateTotalPage()
        {
            TotalPages = (int)Math.Ceiling(CountItems / (double)PageSize);
        }
    }
}
