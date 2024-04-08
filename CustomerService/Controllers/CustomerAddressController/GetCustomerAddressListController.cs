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
                //List<CustomerAddress> customers = await _unitOfWork.CustomerAddress.GetAllPageing(pageIndex, pageSize);

                //List<ViewCustomerAddressDto> viewCustomerDtos = new();
                //foreach (Customer item in customers)
                //{
                //    viewCustomerDtos.Add(new ViewCustomerAddressDto
                //    {
                //        CreateTime = item.CreateTime,
                //        Email = item.Email,
                //        Id = item.Id,
                //        Name = item.Name,
                //        Password = item.Password,
                //        PhoneNumber = item.PhoneNumber,
                //        UpdateTime = item.UpdateTime,
                //    });
                //}
                //return Ok(viewCustomerDtos);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
