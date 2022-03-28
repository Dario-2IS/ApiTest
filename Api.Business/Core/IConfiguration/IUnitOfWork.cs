using Api.Business.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Business.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IAccountRepository AccountRepository { get; }
        IMovementRepository MovementRepository { get; }

        Task CompleteAsync();

    }
}
