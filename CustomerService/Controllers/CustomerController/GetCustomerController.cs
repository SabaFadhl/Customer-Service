using CustomerService.Application.Dto.Customer;
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
    [Route("api/Customer/")]
    public class GetCustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult> Get(string customerId)
        {
            try
            {
                Customer customer = await _unitOfWork.GetRepository<Customer>().GetById(customerId);

                if (customer != null)
                {
                    ViewCustomerAddressDto viewCustomerDto = new ViewCustomerAddressDto
                    {
                        PhoneNumber = customer.PhoneNumber,
                        Id = customer.Id,
                        Name = customer.Name,
                        Email = customer.Email,
                        CreateTime = DateTime.Now,
                        Password = customer.Password,
                        UpdateTime = customer.UpdateTime,
                    };

                    return Ok(viewCustomerDto);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
