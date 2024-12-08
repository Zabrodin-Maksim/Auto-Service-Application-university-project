using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class Employer
    {
        public int EmployerId { get; set; }
        public string Speciality { get; set; }
        public string NameEmployee { get; set; }
        public int Phone { get; set; }
        public Office Office { get; set; } // Связь с Office
        public Employer Supervisor { get; set; } // Связь с другим Employer (если есть)
        public Address Address { get; set; }

        public override string ToString()
        {
            return $"Speciality: {Speciality}, Name Employee: {NameEmployee}, Phone: +420 {Phone}, Supervisor: {Supervisor}";
        }
    }
}
