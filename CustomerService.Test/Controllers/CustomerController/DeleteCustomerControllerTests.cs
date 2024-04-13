using AutoFixture;
using AutoMapper;
using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Interface;
using CustomerService.Controllers.CustomerController;
using CustomerService.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
 

namespace CustomerService.Test.Controllers.CustomerController
{
    public class DeleteCustomerControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;       
        private readonly DeleteCustomerController _controller;
       
        public DeleteCustomerControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();          
            _controller = new DeleteCustomerController(_serviceMock.Object);
        }

        [Fact]
        public void DeleteCustomer_Should_Return_NoContent_When_Delete()
        {
            // Arrange           
            Customer request = _fixture.Create<Customer>();
            _serviceMock.Setup(x => x.Customer.GetById(request.Id)).ReturnsAsync(request);
            _serviceMock.Setup(x => x.Customer.Remove(request)).Verifiable();

            // Act
            var result = _controller.Delete(request.Id);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NoContentResult>(result.Result);
            Assert.Equal(204, objectResult.StatusCode);
        }

        [Fact]
        public void DeleteCustomer_Should_Return_NotFound_When_Id_is_Notfount()
        {
            // Arrange           
            var customerId = _fixture.Create<Guid>().ToString();           
            _serviceMock.Setup(x => x.Customer.GetById(customerId)).ReturnsAsync((Customer)null);

            // Act
            var result = _controller.Delete(customerId);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, objectResult.StatusCode);
        }
    }
}
