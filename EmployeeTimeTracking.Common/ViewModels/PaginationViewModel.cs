using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeTracking.Common.ViewModels
{
   /// <summary>
   /// Модель представления пагинации страницы со списком объектов
   /// </summary>
   /// <typeparam name="T">Тип объекта списка</typeparam>
    public class PaginationViewModel<T> where T : class
    {
        public PaginationViewModel()
        {
            List = new List<T>();
        }
        /// <summary>
        /// Список выводимых на страницу объектов
        /// </summary>
        public ICollection<T> List { get; set; }
        /// <summary>
        /// Модель представления информации о странице
        /// </summary>
        public PageInfoViewModel PageInfo { get; set; }

        /// <summary>
        /// Метод, реализующий постраничный вывод списка объектов
        /// </summary>
        /// <param name="list">Список выводимых на страницу объектов</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы (сколько объектов выводится на странице)</param>
        /// <returns>Список объектов, которые должны быть на текущей странице</returns>
        public IEnumerable<T> Pagination(IEnumerable<T> list, int pageNumber, int pageSize)
        {
            return list.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
