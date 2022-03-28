using Api.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Core.IRepository
{
    public interface IBaseRepository <T> where T : class
    {
        Task<Response<List<T>>> GetAll();
        Task<Response<T>> GetById(int id);
        Task<Response<string>> Add(T dto);
        Task<Response<string>> Update(T dto);
        Task<Response<string>> Delete(int id);
    }
}
