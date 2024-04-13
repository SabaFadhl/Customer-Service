using AutoFixture;
using AutoMapper;
using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Interface;
using CustomerService.Controllers.CustomerAddressController;
using CustomerService.Controllers.CustomerController;
using CustomerService.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Test.Controllers.CustomerAddressController
{
    public class GetCustomerAddressControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly GetCustomerAddressController _controller;

        public GetCustomerAddressControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _controller = new GetCustomerAddressController(_serviceMock.Object);
        }

        [Fact]
        public void GetCustomerAddress_Should_Return_Ok_With_Customer()
        {
            // Arrange           
            CustomerAddress request = _fixture.Create<CustomerAddress>();
            _serviceMock.Setup(x => x.CustomerAddress.GetById(request.Id)).ReturnsAsync(request);

            Customer requestCustomer = _fixture.Create<Customer>();
            _serviceMock.Setup(x => x.Customer.GetById(request.CustomerId)).ReturnsAsync(requestCustomer);

            // Act
            var result = _controller.Get(request.Id);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void GetCustomer_Should_Return_NotFound_When_Wrong_Id()
        {
            // Arrange           
            CustomerAddress request = _fixture.Create<CustomerAddress>();
            _serviceMock.Setup(x => x.CustomerAddress.GetById(request.Id)).ReturnsAsync((CustomerAddress)null);

            Customer requestCustomer = _fixture.Create<Customer>();
            _serviceMock.Setup(x => x.Customer.GetById(request.CustomerId)).ReturnsAsync(requestCustomer);

            // Act
            var result = _controller.Get(request.Id);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NotFoundResult>(result.Result);
            Assert.Equal(404, objectResult.StatusCode);
        }
    }
}
