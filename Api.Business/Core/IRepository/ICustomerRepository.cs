using Api.DataAccess.Entities;
using Api.Models.Dtos.Customer;
using Api.Models.Request.Utils;
using Api.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Core.IRepository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Response<List<DetailMovement>>> GetReport(ReportRequest dto);
    }
}
