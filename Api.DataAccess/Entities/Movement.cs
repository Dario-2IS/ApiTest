using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DataAccess.Entities
{
    public class Movement : Entity
    {
        public DateTime Date{ get; set; }
        public string MovementType { get; set; }
        public decimal Mount { get; set; }
        public decimal CurrentBalance { get; set; }
        public Account Account { get; set; }
    }
}
