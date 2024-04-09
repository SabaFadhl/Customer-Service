using AutoFixture;
using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Interface;
using CustomerService.Controllers.CustomerController;
using CustomerService.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Test.Controllers.CustomerController
{
    public class UpdateCuatomerControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly UpdateCuatomerController _controller;

        public UpdateCuatomerControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _controller = new UpdateCuatomerController(_serviceMock.Object);
        }

        [Fact]
        public void UpdateCuatomer_Should_Return_NoContent()
        {
            // Arrange           
            Customer request = _fixture.Create<Customer>();
            _serviceMock.Setup(x => x.Customer.GetById(request.Id)).ReturnsAsync(request);

            var requestDto = _fixture.Create<UpdateCustomerDto>();
            requestDto.PhoneNumber = "+967 123456789";

            // Act
            var result = _controller.Update(request.Id, requestDto);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NoContentResult>(result.Result);
            Assert.Equal(204, objectResult.StatusCode);
        }

        [Fact]
        public void UpdateCuatomer_Should_Return_NotFound_When_Wrong_CustomerId()
        {
            // Arrange           
            Customer request = _fixture.Create<Customer>();
            _serviceMock.Setup(x => x.Customer.GetById(request.Id)).ReturnsAsync((Customer)null);

            var requestDto = _fixture.Create<UpdateCustomerDto>();
            requestDto.PhoneNumber = "+967 123456789";

            // Act
            var result = _controller.Update(request.Id, requestDto);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, objectResult.StatusCode);
        }
    }
}
