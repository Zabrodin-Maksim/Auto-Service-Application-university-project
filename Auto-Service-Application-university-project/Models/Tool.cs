using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDAS2_University_Project.Models
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
