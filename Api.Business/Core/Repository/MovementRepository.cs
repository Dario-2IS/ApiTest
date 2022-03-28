using Api.Business.Core.IRepository;
using Api.DataAccess;
using Api.DataAccess.Entities;
using Api.Models.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Core.Repository
{
    public class MovementRepository : BaseRepository<Movement>, IMovementRepository
    {
        public MovementRepository(DataContext context) : base(context)
        {
        }

        public override async Task<Response<List<Movement>>> GetAll()
        {
            Response<List<Movement>> response = new();
            try
            {
                var result = await _dbSet
                    .Include(ac => ac.Account)
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
        public override async Task<Response<Movement>> GetById(int id)
        {
            Response<Movement> response = new();
            try
            {
                var result = await _dbSet
                    .Include(ac => ac.Account)
                    .FirstOrDefaultAsync(x => x.Id == id);

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
        public override async Task<Response<string>> Add(Movement dto)
        {
            Response<string> response = new();
            using (var trx = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //Validate limit
                    if (await validateLimit(dto))
                        throw new Exception(Constants.Constants.MovementsControls.AmountError);
                    //1. Prepare Detail
                    dto.CurrentBalance = prepareData(dto);
                    if (dto.CurrentBalance <= 0)
                        throw new Exception(Constants.Constants.MovementsControls.BalanceError);
                    //2. Update Balance
                    dto.Account.Balance = dto.CurrentBalance;
                    //3. Create Movement
                    await _dbSet.AddAsync(dto);
                    
                    response.Data = null;
                    response.Message = Constants.Constants.ResponseConstants.Success;
                    await trx.CommitAsync();
                }
                catch (Exception e)
                {
                    await trx.RollbackAsync();
                    response.Success = !response.Success;
                    response.Message = e.Message;
                }
            }
            return response;
        }
        public override async Task<Response<string>> Update(Movement dto)
        {
            Response<string> response = new();
            try
            {
                var result = await _dbSet
                    .FindAsync(dto.Id);

                if (result != null)
                {
                    result.Date = dto.Date;
                    result.MovementType = dto.MovementType;
                    result.Mount = dto.Mount;
                    result.CurrentBalance = dto.CurrentBalance;

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

        public decimal prepareData(Movement movement)
        {
            var result = 0M;
            try
            {
                var account = _context.Accounts
                    .FirstOrDefault(x => x.Id == movement.Account.Id);
                movement.Account = account;
                result = account.Balance;

                if (result < movement.Mount)
                    throw new Exception(Constants.Constants.MovementsControls.BalanceError);
                
                if (movement.MovementType.ToUpper() == Constants.Constants.MovementsControls.Debit)
                    result -= movement.Mount;
                else if(movement.MovementType.ToUpper() == Constants.Constants.MovementsControls.Credit)
                    result += movement.Mount;
                else
                    throw new Exception(Constants.Constants.MovementsControls.NotValid);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> validateLimit(Movement movement)
        {
            var result = await _dbSet
                    .Include(ac => ac.Account)
                    .ToListAsync();

            decimal limit = 0;
            foreach (var item in result.Where(x => x.Account.Id == movement.Account.Id))
            {
                limit += item.Mount;
            }
            if (limit >= Constants.Constants.MovementsControls.DailyAmount)
                return true;
            return false;
        }
    }
}
