using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DataAccess.Entities
{
    public class Customer : Person
    {
        public string Password { get; set; }
        public bool State { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
