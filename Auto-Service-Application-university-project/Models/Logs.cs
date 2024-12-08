using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class Logs
    {
        public int LogId { get; set; }
        public string TableName { get; set; }
        public string Operation { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
