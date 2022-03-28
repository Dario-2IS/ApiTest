using Api.Models.Dtos.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Dtos.Account
{
    public class AccountDto : BaseDto
    {
        public string Number { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public bool State { get; set; }
        public CustomerDto Customer { get; set; }
    }
}
