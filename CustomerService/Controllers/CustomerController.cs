using CustomerService.Application.Dto;
using CustomerService.Application.Interface;
using CustomerService.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Xml.Linq;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
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
                return BadRequest(new { errorMessage = "You must enter a Name of Customer." });
            }
            if (string.IsNullOrWhiteSpace(addCustomerDto.Email))
            {
                return BadRequest(new { errorMessage = "You must enter a Email of Customer." });
            }
            if (string.IsNullOrWhiteSpace(addCustomerDto.PhoneNumber))
            {
                return BadRequest(new { errorMessage = "You must enter a PhoneNumber of Customer." });
            }
            if (string.IsNullOrWhiteSpace(addCustomerDto.Password))
            {
                return BadRequest(new { errorMessage = "You must enter a Password of Customer." });
            }
            #endregion

            try
            {
                if ((await _unitOfWork.GetRepository<Customer>().SingleOrDefaultAsync(c => c.Name == addCustomerDto.Name || c.Email == addCustomerDto.Email)) != null)
                {
                    return BadRequest(new { errorMessage = "This Customer already exists with the same name or email." });
                }

                Customer customer = new Customer
                {
                    Name = addCustomerDto.Name,
                    Email = addCustomerDto.Email,
                    Password = addCustomerDto.Password,
                    PhoneNumber = addCustomerDto.PhoneNumber
                };

                _unitOfWork.GetRepository<Customer>().Add(customer);

                await _unitOfWork.SaveChangesAsync();

                return Ok(new { customer.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult> Get(string customerId)
        {
            try
            {
                Customer customer = await _unitOfWork.GetRepository<Customer>().GetById(customerId);

                if (customer != null)
                {
                    ViewCustomerDto viewCustomerDto = new ViewCustomerDto
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

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int pageIndex, int pageSize)
        {
            try
            {
                List<Customer> customers = await _unitOfWork.GetRepository<Customer>().GetAllPageing(pageIndex, pageSize);
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> Update(string customerId, UpdateCustomerDto updateCustomerDto)
        {
            #region Validation Fields
            if (updateCustomerDto == null)
            {
                return BadRequest(new { errorMessage = "The Customer is null." });
            }
            if (string.IsNullOrWhiteSpace(updateCustomerDto.Name))
            {
                return BadRequest(new { errorMessage = "You must enter a Name of Customer." });
            }
            if (string.IsNullOrWhiteSpace(updateCustomerDto.Email))
            {
                return BadRequest(new { errorMessage = "You must enter a Email of Customer." });
            }
            if (string.IsNullOrWhiteSpace(updateCustomerDto.PhoneNumber))
            {
                return BadRequest(new { errorMessage = "You must enter a PhoneNumber of Customer." });
            }
            if (string.IsNullOrWhiteSpace(updateCustomerDto.Password))
            {
                return BadRequest(new { errorMessage = "You must enter a Password of Customer." });
            }
            #endregion

            try
            {               
                Customer customer = new Customer
                {
                    Id = customerId,
                    Name = updateCustomerDto.Name,
                    Email = updateCustomerDto.Email,
                    Password = updateCustomerDto.Password,
                    PhoneNumber = updateCustomerDto.PhoneNumber
                };
                if (customer != null)
                {
                    _unitOfWork.GetRepository<Customer>().Update(customer);

                    await _unitOfWork.SaveChangesAsync();

                    return NoContent();
                }
                else
                {
                    return NotFound();
                }               
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> Delete(string customerId)
        {            
            try
            {
                Customer customer = await _unitOfWork.GetRepository<Customer>().GetById(customerId);
                if (customer != null)
                {
                    _unitOfWork.GetRepository<Customer>().Remove(customer);

                    await _unitOfWork.SaveChangesAsync();

                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
