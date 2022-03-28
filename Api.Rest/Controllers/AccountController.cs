using Api.Business.Core.IConfiguration;
using Api.DataAccess.Entities;
using Api.Models.Dtos.Account;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        //Dependency Injection
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _unitOfWork.AccountRepository.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await _unitOfWork.AccountRepository.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AccountDto accountDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _unitOfWork.AccountRepository.Add(accountDto.Adapt<Account>());
                if (result.Success)
                {
                    await _unitOfWork.CompleteAsync();
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState.ValidationState);
        }

        [HttpPut]
        public async Task<IActionResult> Update(AccountDto accountDto)
        {
            if (ModelState.IsValid)
            {
                var customer = await _unitOfWork.CustomerRepository.GetById(accountDto.Customer.Id);
                if (!customer.Success)
                {
                    return BadRequest(customer);
                }
                customer.Data.Adapt(accountDto.Customer);
                var result = await _unitOfWork.AccountRepository.Update(accountDto.Adapt<Account>());
                if (result.Success)
                {
                    await _unitOfWork.CompleteAsync();
                    return Ok(result);
                }
                
            }
            return BadRequest(ModelState.ValidationState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.AccountRepository.Delete(id);
            if (result.Success)
            {
                await _unitOfWork.CompleteAsync();
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
