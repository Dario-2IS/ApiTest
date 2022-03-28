using Api.DataAccess.Entities;
using Api.Models.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Dtos.Customer
{
    public class CustomerDto : BaseDto
    {
        public string DocumentId { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool State { get; set; }
        public List<AccountDto> Accounts { get; set; }
    }
}
