using AutoFixture;
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
    public class GetCustomerAddressListControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly GetCustomerAddressListController _controller;

        public GetCustomerAddressListControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _controller = new GetCustomerAddressListController(_serviceMock.Object);
        }

        [Fact]
        public void GetCustomer_Should_Return_Ok_With_Customer_List()
        {
            // Arrange           
            List<CustomerAddress> request = _fixture.Create<List<CustomerAddress>>();
            _serviceMock.Setup(x => x.CustomerAddress.GetAllPageing(1, 5)).ReturnsAsync(request);

            // Act
            var result = _controller.GetList(1, 5);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
        }       
    }
}
