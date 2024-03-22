using CustomerService.Application.Dto;
using CustomerService.Application.Interface;
using CustomerService.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Xml.Linq;

namespace CustomerService.Controllers.CustomerController
{
    [ApiController]
    [Route("api/Customer")]
    public class AddCustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCustomerDto addCustomerDto)
        {
            #region Validation Fields
            if (addCustomerDto == null)
            {
                return BadRequest(new { errorMessage = "The Customer is null." });
            }
            if (string.IsNullOrWhiteSpace(addCustomerDto.Name))
            {
                return BadRequest(new { errorMessage = "You must enter Name of the Customer." });
            }
            if (string.IsNullOrWhiteSpace(addCustomerDto.Email))
            {
                return BadRequest(new { errorMessage = "You must enter Email of the Customer." });
            }
            if (string.IsNullOrWhiteSpace(addCustomerDto.PhoneNumber))
            {
                return BadRequest(new { errorMessage = "You must enter PhoneNumber of the Customer." });
            }
            if (string.IsNullOrWhiteSpace(addCustomerDto.Password))
            {
                return BadRequest(new { errorMessage = "You must enter Password of the Customer." });
            }
            #endregion

            try
            {
                if ((await _unitOfWork.GetRepository<Customer>().SingleOrDefaultAsync(c => c.Name == addCustomerDto.Name || c.Email == addCustomerDto.Email)) != null)
                {
                    return BadRequest(new { errorMessage = "This Customer already exists with the same name or email." });
                }

                Customer customer = new Customer
                {
                    Name = addCustomerDto.Name,
                    Email = addCustomerDto.Email,
                    Password = addCustomerDto.Password,
                    PhoneNumber = addCustomerDto.PhoneNumber
                };

                _unitOfWork.GetRepository<Customer>().Add(customer);

                await _unitOfWork.SaveChangesAsync();

                return Ok(new { customer.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
