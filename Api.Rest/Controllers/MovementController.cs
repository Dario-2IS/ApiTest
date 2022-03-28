using Api.Business.Core.IConfiguration;
using Api.DataAccess.Entities;
using Api.Models.Dtos.Movement;
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
    public class MovementController : Controller
    {
        //Dependency Injection
        private readonly IUnitOfWork _unitOfWork;

        public MovementController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _unitOfWork.MovementRepository.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await _unitOfWork.MovementRepository.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MovementDto movementDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _unitOfWork.MovementRepository.Add(movementDto.Adapt<Movement>());
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
        public async Task<IActionResult> Update(MovementDto movementDto)
        {
            if (ModelState.IsValid)
            {
                var account = await _unitOfWork.MovementRepository.GetById(movementDto.Id);
                if (!account.Success)
                {
                    return BadRequest(account);
                }
                account.Data.Adapt(movementDto.Account);
                var result = await _unitOfWork.AccountRepository.Update(movementDto.Adapt<Account>());
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
            var result = await _unitOfWork.MovementRepository.Delete(id);
            if (result.Success)
            {
                await _unitOfWork.CompleteAsync();
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
