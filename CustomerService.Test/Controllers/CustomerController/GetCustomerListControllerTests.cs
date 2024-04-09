using AutoFixture;
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
    public class GetCustomerListControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly GetCustomerListController _controller;

        public GetCustomerListControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _controller = new GetCustomerListController(_serviceMock.Object);
        }

        [Fact]
        public void GetCustomer_Should_Return_Ok_With_Customer_List()
        {
            // Arrange           
            List<Customer> request = _fixture.Create<List<Customer>>();
            _serviceMock.Setup(x => x.Customer.GetAllPageing(1, 5)).ReturnsAsync(request);

            // Act
            var result = _controller.GetList(1, 5);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
        }       
    }
}
