using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Response
{
    public abstract class ResponseBase
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
    }
}
