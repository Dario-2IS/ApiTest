using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models.Dtos
{
    public abstract class BaseDto
    {
        public virtual int Id { get; set; }
    }
}
