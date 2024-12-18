using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Service_Application_university_project.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public Bill Bill { get; set; }
        public Client Client { get; set; }
        public PaymentType PaymentType { get; set; }

        public Card Card { get; set; }

        public Cash Cash { get; set; }

        public override string ToString()
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 5801);
            if (Bill.Price == 0)
            {
                return $"Bill: Date Bill: 16.10.2024, Price: {randomNumber}, Payment Type: {PaymentType}, Client: {Client.ClientName}";
            }
            return $"Bill: {Bill}, Payment Type: {PaymentType}";
        }
    }
}
