using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class ServiseOffer
    {
        public int OfferId { get; set; }                 // Первичный ключ
        public decimal PricePerHour { get; set; }        // Цена за час
        public DateTime DateOffer { get; set; }          // Дата предложения
        public int EmployerEmployerId { get; set; }      // Ссылка на сотрудника
        public int CarCarId { get; set; }                // Ссылка на автомобиль
        public int ServiceTypeId { get; set; }           // Ссылка на тип услуги
        public decimal WorkingHours { get; set; }        // Количество рабочих часов
    }
}
