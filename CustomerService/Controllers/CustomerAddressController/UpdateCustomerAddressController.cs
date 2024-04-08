using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Dto.CustomerAddress;
using CustomerService.Application.Interface;
using CustomerService.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CustomerService.Controllers.CustomerAddressController
{
    [Route("api/CustomerAddress")]
    [ApiController]
    public class UpdateCustomerAddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerAddressController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> Update(string customerId, UpdateCustomerAddressDto updateCustomerDto)
        {
            #region Validation Fields
            if (updateCustomerDto == null)
            {
                return BadRequest(new { errorMessage = "The Customer is null." });
            }
            if (string.IsNullOrWhiteSpace(updateCustomerDto.Address))
            {
                return BadRequest(new { errorMessage = "You must enter Address." });
            }
           
            #endregion

            try
            {
                CustomerAddress customer = await _unitOfWork.CustomerAddress.GetById(customerId);                                                 
                if (customer != null)
                {                   
                    //customer.Name = updateCustomerDto.Name;
                    //customer.Email = updateCustomerDto.Email;
                    //customer.Password = updateCustomerDto.Password;
                    //customer.PhoneNumber = updateCustomerDto.PhoneNumber;

                    //_unitOfWork.GetRepository<Customer>().Update(customer);

                    //await _unitOfWork.SaveChangesAsync();

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
