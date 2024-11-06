using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Models
{
    public class Employer
    {
        public int EmployerId { get; set; }             // Первичный ключ
        public string NameEmployee { get; set; }        // Имя сотрудника
        public string Speciality { get; set; }          // Специальность
        public string Phone { get; set; }               // Телефон
        public int OfficeOfficeId { get; set; }         // Ссылка на офис
        public int? EmployerEmployerId { get; set; }    // Ссылка на начальника (nullable)
        public int AddressAddressId { get; set; }       // Ссылка на адрес
    }
}
