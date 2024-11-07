using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class Tool
    {
        public int ToolId { get; set; }
        public string Speciality { get; set; }
        public decimal Price { get; set; }
        public DateTime CheckDate { get; set; }
        public int OfficeOfficeId { get; set; }
    }
}
