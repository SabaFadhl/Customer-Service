using AutoFixture;
using AutoMapper;
using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Interface;
using CustomerService.Controllers.CustomerAddressController;
using CustomerService.Controllers.CustomerController;
using CustomerService.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using RestAssured.Response.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
 

namespace CustomerService.Test.Controllers.CustomerAddressController
{
    public class DeleteCustomerAddressControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;       
        private readonly DeleteCustomerAddressController _controller;
       
        public DeleteCustomerAddressControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();          
            _controller = new DeleteCustomerAddressController(_serviceMock.Object);
        }

        [Fact]
        public void DeleteCustomerAddress_Should_Return_NoContent_When_Delete()
        {
            // Arrange           
            CustomerAddress request = _fixture.Create<CustomerAddress>();
            _serviceMock.Setup(x => x.CustomerAddress.GetById(request.Id)).ReturnsAsync(request);
            _serviceMock.Setup(x => x.CustomerAddress.Remove(request)).Verifiable();

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
            var customerAddressId = _fixture.Create<Guid>().ToString();           
            _serviceMock.Setup(x => x.CustomerAddress.GetById(customerAddressId)).ReturnsAsync((CustomerAddress)null);

            // Act
            var result = _controller.Delete(customerAddressId);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NotFoundResult>(result.Result);
            Assert.Equal(404, objectResult.StatusCode);
        }
    }
}
