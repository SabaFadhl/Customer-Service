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

        [HttpPut("{customerAddressId}")]
        public async Task<IActionResult> Update(string customerAddressId, UpdateCustomerAddressDto updateCustomerAddressDto)
        {
            #region Validation Fields
            if (updateCustomerAddressDto == null)
            {
                return BadRequest(new { errorMessage = "The Customer is null." });
            }
            if (string.IsNullOrWhiteSpace(updateCustomerAddressDto.Address))
            {
                return BadRequest(new { errorMessage = "You must enter Address." });
            }
           
            #endregion

            try
            {
                CustomerAddress customerAddress = await _unitOfWork.CustomerAddress.GetById(customerAddressId);                                                 
                if (customerAddress != null)
                {
                    customerAddress.Address = updateCustomerAddressDto.Address;
                    customerAddress.UpdateTime = DateTime.Now;
                    customerAddress.GeoLat = updateCustomerAddressDto.GeoLat;
                    customerAddress.GeoLon = updateCustomerAddressDto.GeoLon;
                    customerAddress.Id = customerAddressId;

                    _unitOfWork.CustomerAddress.Update(customerAddress);

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
