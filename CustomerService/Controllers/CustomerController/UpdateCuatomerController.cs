using CustomerService.Application.Dto;
using CustomerService.Application.Interface;
using CustomerService.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CustomerService.Controllers.CustomerController
{
    [Route("api/Customer")]
    [ApiController]
    public class UpdateCuatomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCuatomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> Update(string customerId, UpdateCustomerDto updateCustomerDto)
        {
            #region Validation Fields
            if (updateCustomerDto == null)
            {
                return BadRequest(new { errorMessage = "The Customer is null." });
            }
            if (string.IsNullOrWhiteSpace(updateCustomerDto.Name))
            {
                return BadRequest(new { errorMessage = "You must enter Name of the Customer." });
            }
            if (string.IsNullOrWhiteSpace(updateCustomerDto.Email))
            {
                return BadRequest(new { errorMessage = "You must enter Email of the Customer." });
            }
            
            if (string.IsNullOrWhiteSpace(updateCustomerDto.PhoneNumber))
            {
                return BadRequest(new { errorMessage = "You must enter PhoneNumber of the Customer." });
            }
            else
            {
                if (!Regex.IsMatch(updateCustomerDto.PhoneNumber, @"^\+967\s\d{9}$"))
                {
                    return BadRequest(new { errorMessage = "Invalid PhoneNumber, The PhoneNumber must be like this format: +000 000000000" });
                }
            }
            if (string.IsNullOrWhiteSpace(updateCustomerDto.Password))
            {
                return BadRequest(new { errorMessage = "You must enter Password of the Customer." });
            }
            #endregion

            try
            {
                Customer customer = await _unitOfWork.GetRepository<Customer>().GetById(customerId);                                                 
                if (customer != null)
                {                   
                    customer.Name = updateCustomerDto.Name;
                    customer.Email = updateCustomerDto.Email;
                    customer.Password = updateCustomerDto.Password;
                    customer.PhoneNumber = updateCustomerDto.PhoneNumber;

                    _unitOfWork.GetRepository<Customer>().Update(customer);

                    await _unitOfWork.SaveChangesAsync();

                    return NoContent();
                }
                else
                {
                    return NotFound(new { ErrorMesssage="There is no Customer with given Id."});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
