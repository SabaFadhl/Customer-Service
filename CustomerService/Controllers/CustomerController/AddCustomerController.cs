using CustomerService.Application.Dto;
using CustomerService.Application.Interface;
using CustomerService.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
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
            else
            {
                if (!Regex.IsMatch(addCustomerDto.PhoneNumber, @"^\+967\s\d{9}$"))
                {
                    return BadRequest(new { errorMessage = "Invalid PhoneNumber, The PhoneNumber must be like this format: +000 000000000" });
                }
            }
            if (string.IsNullOrWhiteSpace(addCustomerDto.Password))
            {
                return BadRequest(new { errorMessage = "You must enter Password of the Customer." });
            }

             
            #endregion

            try
            {
                if ((await _unitOfWork.Customer.SingleOrDefaultAsync(c => c.Name == addCustomerDto.Name || c.Email == addCustomerDto.Email)) != null)
                {
                    return BadRequest(new { errorMessage = "This Customer already exists with the same name or email." });
                }

                Customer customer = new Customer
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = addCustomerDto.Name,
                    Email = addCustomerDto.Email,
                    Password = addCustomerDto.Password,
                    PhoneNumber = addCustomerDto.PhoneNumber
                };

                _unitOfWork.Customer.Add(customer);

                await _unitOfWork.SaveChangesAsync();

                return StatusCode(201, new ReturnGuidDto { Id = customer.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
