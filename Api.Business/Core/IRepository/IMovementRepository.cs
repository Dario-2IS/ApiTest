using Api.DataAccess.Entities;
using Api.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Core.IRepository
{
    public interface IMovementRepository : IBaseRepository<Movement>
    {
        decimal prepareData(Movement movement);
    }
}
