using Api.Business.Core.IConfiguration;
using Api.DataAccess.Entities;
using Api.Models.Dtos.Customer;
using Api.Models.Request.Utils;
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
    public class CustomerController : Controller
    {
        //Dependency Injection
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _unitOfWork.CustomerRepository.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await _unitOfWork.CustomerRepository.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            { 
                var result = await _unitOfWork.CustomerRepository.Add(customerDto.Adapt<Customer>());
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
        public async Task<IActionResult> Update(CustomerDto customerDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _unitOfWork.CustomerRepository.Update(customerDto.Adapt<Customer>());
                if (result.Success)
                {
                    await _unitOfWork.CompleteAsync();
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState.ValidationState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.CustomerRepository.Delete(id);
            if (result.Success)
            {
                await _unitOfWork.CompleteAsync();
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("report")]
        public async Task<IActionResult> GetReport(ReportRequest request)
        {
            var result = await _unitOfWork.CustomerRepository.GetReport(request);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
