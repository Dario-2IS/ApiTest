using Api.Business.Core.IRepository;
using Api.DataAccess;
using Api.DataAccess.Entities;
using Api.Models.Response;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Core.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(DataContext context) : base(context)
        {
        }

        public override async Task<Response<List<Account>>> GetAll()
        {
            Response<List<Account>> response = new();
            try
            {
                var result = await _dbSet
                    .Include(c => c.Customer)
                    .Include(mv => mv.Movements)
                    .ToListAsync();

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
        public override async Task<Response<Account>> GetById(int id)
        {
            Response<Account> response = new();
            try
            {
                var result = await _dbSet
                    .FindAsync(id);

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
        public override async Task<Response<string>> Add(Account dto)
        {
            Response<string> response = new();
            try
            {
                var result = await _dbSet
                    .Include(c => c.Customer)
                    .FirstOrDefaultAsync(x => x.Customer.Id == dto.Customer.Id);

                if (result != null)
                {
                    dto.Customer = result.Customer;
                    await _dbSet.AddAsync(dto);
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
        public override async Task<Response<string>> Update(Account dto)
        {
            Response<string> response = new();
            try
            {
                var result = await _dbSet
                    .FindAsync(dto.Id);

                if (result != null)
                {
                    result.Number = dto.Number;
                    result.AccountType = dto.AccountType;
                    result.Balance = dto.Balance;
                    result.State = dto.State;

                    _dbSet.Update(result);
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
        public override async Task<Response<string>> Delete(int id)
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
    }
}
