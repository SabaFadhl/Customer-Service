using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Interface;
using CustomerService.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Xml.Linq;

namespace CustomerService.Controllers.CustomerAddressController
{
    [ApiController]
    [Route("api/CustomerAddress/")]
    public class GetCustomerAddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerAddressController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult> Get(string customerId)
        {
            try
            {
                CustomerAddress customerAddress = await _unitOfWork.CustomerAddress.GetById(customerId);

                if (customerAddress != null)
                {
                    //ViewCustomerAddressDto viewCustomerDto = new ViewCustomerAddressDto
                    //{
                    //    PhoneNumber = customerAddress.PhoneNumber,
                    //    Id = customer.Id,
                    //    Name = customer.Name,
                    //    Email = customer.Email,
                    //    CreateTime = DateTime.Now,
                    //    Password = customer.Password,
                    //    UpdateTime = customer.UpdateTime,
                    //};

                    // return Ok(viewCustomerDto);
                    return NotFound();
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
