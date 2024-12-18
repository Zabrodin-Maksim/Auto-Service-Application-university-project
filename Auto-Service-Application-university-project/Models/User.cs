using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 
        public string Name { get; set; }
        public int Phone { get; set; }
        public Address Address { get; set; }

        public int RoleId {  get; set; }

        public string ShortAdress { get => Address.City + " " + Address.Street + Address.HouseNumber; }

        public static bool IsSecredModeActive { get; set; }

        public override string ToString()
        {
            if (IsSecredModeActive)
            {
                return $"{Username}, Name: {Name}, Phone: +420 *********, Address: ***..., Role: {RoleId}";
            }
            else
            {
                return $"{Username}, Name: {Name}, Phone: +420 {Phone}, Address: {ShortAdress}, Role: {RoleId}";
            }
        }
    }
}
