using Api.Business.Core.IRepository;
using Api.DataAccess;
using Api.Models.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Core.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DataContext _context;
        protected DbSet<T> _dbSet;

        public BaseRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<Response<string>> Add(T dto)
        {
            Response<string> response = new();
            try
            {
                await _dbSet.AddAsync(dto);
                response.Data = null;
                response.Message = Constants.Constants.ResponseConstants.Success;
            }
            catch (Exception e)
            {
                response.Success = !response.Success;
                response.Message = e.Message;
            }
            return response;
        }

        public virtual async Task<Response<string>> Delete(int id)
        {
            Response<string> response = new();
            try
            {
                var result = await _dbSet.FindAsync(id);
                if (result != null)
                {
                    _dbSet.Remove(result);
                    response.Data = null;
                    response.Message = Constants.Constants.ResponseConstants.Success;
                }
                else
                {
                    response.Data = null;
                    response.Message = Constants.Constants.ResponseConstants.NotFound;
                }
            }
            catch (Exception e)
            {
                response.Success = !response.Success;
                response.Message = e.Message;
            }
            return response;
        }

        public virtual async Task<Response<List<T>>> GetAll()
        {
            Response<List<T>> response = new();
            try
            {
                var result = await _dbSet.ToListAsync();
                if (result != null)
                {
                    response.Data = result;
                    response.Message = Constants.Constants.ResponseConstants.Success;
                }
                else
                {
                    response.Data = result;
                    response.Message = Constants.Constants.ResponseConstants.NotFound;
                }
            }
            catch (Exception e)
            {
                response.Success = !response.Success;
                response.Message = e.Message;
            }
            return response;
        }

        public virtual async Task<Response<T>> GetById(int id)
        {
            Response<T> response = new();
            try
            {
                var result = await _dbSet.FindAsync(id);
                if (result != null)
                {
                    response.Data = result;
                    response.Message = Constants.Constants.ResponseConstants.Success;
                }
                else
                {
                    response.Data = result;
                    response.Message = Constants.Constants.ResponseConstants.NotFound;
                }
            }
            catch (Exception e)
            {
                response.Success = !response.Success;
                response.Message = e.Message;
            }
            return response;
        }

        public virtual Task<Response<string>> Update(T dto)
        {
            throw new NotImplementedException();
        }
    }
}
