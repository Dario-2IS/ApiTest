using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DataAccess.Entities
{
    public class Account : Entity
    {
        public string Number { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public bool State { get; set; }
        public Customer Customer { get; set; }
        public List<Movement> Movements { get; set; }
    }
}
