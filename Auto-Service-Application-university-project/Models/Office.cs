using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class Office
    {
        public int OfficeId { get; set; }
        public Address Address { get; set; }
        public int OfficeSize { get; set; }

        public override string ToString()
        {
            return $"{Address.Country}, {Address.Street}, House number: {Address.HouseNumber},  {Address.IndexAdd}, Size: {OfficeSize}";
        }
    }
}
