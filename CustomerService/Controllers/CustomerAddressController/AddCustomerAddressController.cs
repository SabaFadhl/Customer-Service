using CustomerService.Application.Dto;
using CustomerService.Application.Dto.Common;
using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Dto.CustomerAddress;
using CustomerService.Application.Interface;
using CustomerService.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CustomerService.Controllers.CustomerAddressController
{
    [ApiController]
    [Route("api/CustomerAddress")]
    public class AddCustomerAddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddCustomerAddressController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCustomerAddressDto AddCustomerAddressDto)
        {
            #region Validation Fields
            if (AddCustomerAddressDto == null)
            {
                return BadRequest(new { errorMessage = "The Customer is null." });
            }
            if (string.IsNullOrWhiteSpace(AddCustomerAddressDto.Address))
            {
                return BadRequest(new { errorMessage = "You must enter Address." });
            }
            if (string.IsNullOrWhiteSpace(AddCustomerAddressDto.CustomerId))
            {
                return BadRequest(new { errorMessage = "You must enter CustomerId." });
            }
            #endregion

            try
            {           
                if ((await _unitOfWork.Customer.SingleOrDefaultAsync(c => c.Id == AddCustomerAddressDto.CustomerId) == null))
                {
                    return BadRequest(new { errorMessage = "There is not customer with this Id '" + AddCustomerAddressDto.CustomerId + "'" });
                }

                CustomerAddress customerAddress = new CustomerAddress
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerId = AddCustomerAddressDto.CustomerId,
                    Address = AddCustomerAddressDto.Address,
                    GeoLat = AddCustomerAddressDto.GeoLat,
                    GeoLon = AddCustomerAddressDto.GeoLon,
                    UpdateTime = DateTime.Now
                };

                _unitOfWork.CustomerAddress.Add(customerAddress);

                await _unitOfWork.SaveChangesAsync();

                return StatusCode(201, new ReturnGuidDto { Id = customerAddress.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}