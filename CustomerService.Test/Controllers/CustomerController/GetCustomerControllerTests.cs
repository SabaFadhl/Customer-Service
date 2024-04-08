using AutoFixture;
using AutoMapper;
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
    public class GetCustomerControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly GetCustomerController _controller;

        public GetCustomerControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _controller = new GetCustomerController(_serviceMock.Object);
        }

        [Fact]
        public void GetCustomer_Should_Return_Ok_With_Customer()
        {
            // Arrange           
            Customer request = _fixture.Create<Customer>();
            _serviceMock.Setup(x => x.Customer.GetById(request.Id)).ReturnsAsync(request);           

            // Act
            var result = _controller.Get(request.Id);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void GetCustomer_Should_Return_NotFound_When_Wron_Id()
        {
            // Arrange           
            Customer request = _fixture.Create<Customer>();
            _serviceMock.Setup(x => x.Customer.GetById(request.Id)).ReturnsAsync((Customer)null);

            // Act
            var result = _controller.Get(request.Id);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NotFoundResult>(result.Result);
            Assert.Equal(404, objectResult.StatusCode);
        }
    }
}
