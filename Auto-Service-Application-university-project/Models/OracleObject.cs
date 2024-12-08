using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class OracleObject
    {
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }

        public override string ToString()
        {
            return $"{ObjectName} - {ObjectType}";
        }
    }
}
