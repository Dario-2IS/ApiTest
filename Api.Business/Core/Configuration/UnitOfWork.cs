using Api.Business.Core.IConfiguration;
using Api.Business.Core.IRepository;
using Api.Business.Core.Repository;
using Api.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Core.Configuration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _context;
        public ICustomerRepository CustomerRepository { get; private set; }
        public IAccountRepository AccountRepository { get; private set; }
        public IMovementRepository MovementRepository { get; private set; }

        public UnitOfWork(DataContext context)
        {
            _context = context;
            CustomerRepository = new CustomerRepository(_context);
            AccountRepository = new AccountRepository(_context);
            MovementRepository = new MovementRepository(_context);
        }
 
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
