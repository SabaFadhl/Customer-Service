using CustomerService.Application.Dto;
using CustomerService.Application.Interface;
using CustomerService.Domain;
using CustomerService.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text.RegularExpressions;

namespace CustomerService.Controllers.CustomerController
{

    [ApiController]
    [Route("api/Customer")]
    public class CustomerLoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerLoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerLoginDto loginCustomerLoginDto)
        {
            #region Validation Fields
            if (loginCustomerLoginDto == null)
            {
                return BadRequest(new { errorMessage = "The CustomerLoginDto is null." });
            }
            if (string.IsNullOrWhiteSpace(loginCustomerLoginDto.EmailOrName))
            {
                return BadRequest(new { errorMessage = "You must enter Email or Name." });
            }
            if (string.IsNullOrWhiteSpace(loginCustomerLoginDto.Password))
            {
                return BadRequest(new { errorMessage = "You must enter Password." });
            }
            #endregion

            try
            {
                Customer customer = await _unitOfWork.Customer.SingleOrDefaultAsync(u => (u.Email == loginCustomerLoginDto.EmailOrName || u.Name == loginCustomerLoginDto.EmailOrName) && u.Password == loginCustomerLoginDto.Password);
                if (customer != null)
                {
                    // Successfully authenticated login                    
                    return StatusCode(200, new { customer.Id });
                }
                else
                {  // Failed to log in
                    return BadRequest(new { errorMessage = "You  failed to log in with the wrong name or email." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }



    }
}
