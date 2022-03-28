using Api.Models.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Dtos.Movement
{
    public class MovementDto : BaseDto
    {
        public DateTime Date { get; set; }
        public string MovementType { get; set; }
        public decimal Mount { get; set; }
        public decimal CurrentBalance { get; set; }
        public AccountDto Account { get; set; }
    }
}
