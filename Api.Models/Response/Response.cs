using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Response
{
    public class Response <T> : ResponseBase
    {
        public T Data { get; set; }
    }
}
