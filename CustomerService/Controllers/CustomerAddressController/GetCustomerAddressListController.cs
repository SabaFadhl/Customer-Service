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
    [Route("api/CustomerAddress")]
    public class GetCustomerAddressListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerAddressListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int pageIndex, int pageSize)
        {
            try
            {
                List<CustomerAddress> customerAddresses = await _unitOfWork.CustomerAddress.GetAllPageing(pageIndex, pageSize);

                List<ViewCustomerAddressDto> viewCustomerAddressDtos = new();
                foreach (CustomerAddress item in customerAddresses)
                {
                    viewCustomerAddressDtos.Add(new ViewCustomerAddressDto
                    {
                        Address = item.Address,
                        Id = item.Id,
                        CreateTime = item.CreateTime,
                        CustomerId = item.CustomerId,
                        GeoLat = item.GeoLat,
                        GeoLon = item.GeoLon,
                        UpdateTime = item.UpdateTime,
                    });
                }
                return Ok(viewCustomerAddressDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}