using CustomerService.Application.Dto;
using CustomerService.Application.Interface;
using CustomerService.Domain;
using CustomerService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Xml.Linq;

namespace CustomerService.Controllers.CustomerController
{
    [Route("api/Customer")]
    [ApiController]
    public class DeleteCustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> Delete(string customerId)
        {
            try
            {             
                Customer customer = await _unitOfWork.Customer.GetById(customerId);
                if (customer != null)
                {
                    _unitOfWork.Customer.Remove(customer);

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
