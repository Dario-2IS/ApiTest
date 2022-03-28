using Api.Business.Core.IRepository;
using Api.DataAccess;
using Api.DataAccess.Entities;
using Api.Models.Dtos.Customer;
using Api.Models.Request.Utils;
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
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext context) : base(context)
        {
        }

        public override async Task<Response<List<Customer>>> GetAll()
        {
            Response<List<Customer>> response = new();
            try
            {
                var result = await _dbSet
                    .Include(ac => ac.Accounts)
                    .ToListAsync();

                if (result != null)
                {
                    response.Data = result;
                    response.Message = Constants.Constants.ResponseConstants.Success;
                }
                else
                {
                    response.Success = !response.Success;
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
        public override async Task<Response<Customer>> GetById(int id)
        {
            Response<Customer> response = new();
            try
            {
                var result = await _dbSet
                    .Include(ac => ac.Accounts)
                    .ThenInclude(mv => mv.Movements)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (result != null)
                {
                    response.Data = result;
                    response.Message = Constants.Constants.ResponseConstants.Success;
                }
                else
                {
                    response.Success = !response.Success;
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
        public override async Task<Response<string>> Add(Customer dto)
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
        public override async Task<Response<string>> Update(Customer dto)
        {
            Response<string> response = new();
            try
            {
                var result = await _dbSet.FindAsync(dto.Id);
                if (result != null)
                {
                    var customerUpdate = dto.Adapt(result);
                    _dbSet.Update(customerUpdate);
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
                    response.Success = !response.Success;
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

        public async Task<Response<List<DetailMovement>>> GetReport(ReportRequest dto)
        {
            Response<List<DetailMovement>> response = new();
            try
            {
                var result = await _dbSet
                   .Include(ac => ac.Accounts)
                   .ThenInclude(mv => mv.Movements)
                   .FirstOrDefaultAsync(x => x.Id == dto.CustomerId);

                if (result != null)
                {
                    List<DetailMovement> movements = new();
                    DetailMovement movement = new();
                    foreach (var detail in result.Accounts)
                    {
                        foreach (var mov in detail.Movements.Where(dt => dt.Date >= dto.Start && dt.Date <= dto.End))
                        {
                            movement.AccountNumber = detail.Number;
                            movement.AccountType = detail.AccountType;
                            movement.CurrentBalance = detail.Balance;
                            movement.CustomerName = detail.Customer.Name;
                            movement.State = detail.State;
                            movement.Amount = mov.Mount;
                            movement.Date = mov.Date;
                            movement.MovementType = mov.MovementType;
                            movements.Add(movement);
                        }
                    }
                    response.Data = movements;
                    response.Message = Constants.Constants.ResponseConstants.Success;
                }
                else
                {
                    response.Success = !response.Success;
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
