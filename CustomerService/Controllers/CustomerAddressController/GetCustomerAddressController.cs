using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Dto.CustomerAddress;
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

        [HttpGet("{customerAddressId}")]
        public async Task<ActionResult> Get(string customerAddressId)
        {
            try
            {
                CustomerAddress customerAddress = await _unitOfWork.CustomerAddress.GetById(customerAddressId);

                if (customerAddress != null)
                {
                    ViewCustomerAddressDto viewCustomerAddressDto = new ViewCustomerAddressDto
                    {
                        Address = customerAddress.Address,
                        Id = customerAddress.Id,
                        CreateTime = customerAddress.CreateTime,
                        CustomerId = customerAddress.Id,
                        GeoLat = customerAddress.GeoLat,
                        GeoLon = customerAddress.GeoLon,
                        UpdateTime = customerAddress.UpdateTime,
                    };

                    return Ok(viewCustomerAddressDto);
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
