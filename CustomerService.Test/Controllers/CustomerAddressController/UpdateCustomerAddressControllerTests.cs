using AutoFixture;
using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Dto.CustomerAddress;
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
    public class UpdateCustomerAddressControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly UpdateCustomerAddressController _controller;

        public UpdateCustomerAddressControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _controller = new UpdateCustomerAddressController(_serviceMock.Object);
        }

        [Fact]
        public void UpdateCuatomerAddress_Should_Return_NoContent()
        {
            // Arrange           
            CustomerAddress request = _fixture.Create<CustomerAddress>();
            _serviceMock.Setup(x => x.CustomerAddress.GetById(request.Id)).ReturnsAsync(request);

            var requestDto = _fixture.Create<UpdateCustomerAddressDto>();            

            // Act
            var result = _controller.Update(request.Id, requestDto);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NoContentResult>(result.Result);
            Assert.Equal(204, objectResult.StatusCode);
        }

        [Fact]
        public void UpdateCuatomerAddress_Should_Return_NotFound_When_Wrong_CustomerAddressId()
        {
            // Arrange           
            CustomerAddress request = _fixture.Create<CustomerAddress>();
            _serviceMock.Setup(x => x.CustomerAddress.GetById(request.Id)).ReturnsAsync((CustomerAddress)null);

            var requestDto = _fixture.Create<UpdateCustomerAddressDto>();            

            // Act
            var result = _controller.Update(request.Id, requestDto);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, objectResult.StatusCode);
        }
    }
}
