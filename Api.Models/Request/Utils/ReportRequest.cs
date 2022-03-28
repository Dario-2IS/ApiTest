using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Request.Utils
{
    public class ReportRequest
    {
        public int CustomerId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
