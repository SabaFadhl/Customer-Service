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
    [Route("api/Customer")]
    public class GetCustomerListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int pageIndex, int pageSize)
        {
            try
            {
                List<Customer> customers = await _unitOfWork.Customer.GetAllPageing(pageIndex, pageSize);

                List<ViewCustomerDto> viewCustomerDtos = new();
                foreach (Customer item in customers)
                {
                    viewCustomerDtos.Add(new ViewCustomerDto
                    {
                        CreateTime = item.CreateTime,
                        Email = item.Email,
                        Id = item.Id,
                        Name = item.Name,
                        Password = item.Password,
                        PhoneNumber = item.PhoneNumber,
                        UpdateTime = item.UpdateTime,
                    });
                }
                return Ok(viewCustomerDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
