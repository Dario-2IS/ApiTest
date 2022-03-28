using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Dtos.Customer
{
    public class DetailMovement
    {
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal CurrentBalance { get; set; }
        public bool State { get; set; }
        public DateTime Date { get; set; }
        public string MovementType { get; set; }
        public decimal Amount { get; set; }
        
    }
}
