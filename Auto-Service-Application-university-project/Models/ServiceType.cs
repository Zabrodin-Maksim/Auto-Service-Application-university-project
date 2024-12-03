using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class ServiceType
    {
        public int ServiceTypeId { get; set; }
        public string TypeName { get; set; }

        public override string ToString()
        {
            return $"{TypeName}";
        }
    }
}
