using AutoFixture;
using AutoMapper;
using Castle.Core.Resource;
using CustomerService.Application.Dto.Common;
using CustomerService.Application.Dto.Customer;
using CustomerService.Application.Dto.CustomerAddress22;
using CustomerService.Application.Interface;
using CustomerService.Controllers.CustomerController;
using CustomerService.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomerService.Test.Controllers.CustomerController
{
    public class AddCustomerControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly AddCustomerController _controller;
        private readonly IMapper _mapperMock;

        public AddCustomerControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<AddCustomerDto, Customer>(It.IsAny<AddCustomerDto>()))
               .Returns((AddCustomerDto source) => new Customer());

            _mapperMock = mapperMock.Object;

            _controller = new AddCustomerController(_serviceMock.Object);
        }

        [Fact]
        public void AddCustomer_ShouldRetunGuid()
        {
            // Arrange
            var request = _fixture.Create<AddCustomerDto>();
            request.PhoneNumber = "+967 123456789";
            var customer = _mapperMock.Map<AddCustomerDto, Customer>(request);            
            _serviceMock.Setup(x => x.Customer.Add(customer)).Verifiable();
           

            // Act
            var result = _controller.Add(request);

            // Assert                       
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result.Result);
            bool isValidGuid = Guid.TryParse(((ReturnGuidDto)objectResult.Value).Id, out Guid guidResult);
            Assert.True(isValidGuid);
        }

        [Fact]
        public void AddCustomer_Should_Return_BadRequest_For_Validate_Name()
        {
            // Arrange
            var request = _fixture.Create<AddCustomerDto>();
            var customer = _mapperMock.Map<AddCustomerDto, Customer>(request);
            _serviceMock.Setup(x => x.Customer.Add(customer)).Verifiable();
            request.Name = null;

            // Act
            var result = _controller.Add(request);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void AddCustomer_Should_Return_BadRequest_For_Validate_Email()
        {
            // Arrange
            var request = _fixture.Create<AddCustomerDto>();
            var customer = _mapperMock.Map<AddCustomerDto, Customer>(request);
            _serviceMock.Setup(x => x.Customer.Add(customer)).Verifiable();
            request.Email = null;

            // Act
            var result = _controller.Add(request);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void AddCustomer_Should_Return_BadRequest_For_Validate_Password()
        {
            // Arrange
            var request = _fixture.Create<AddCustomerDto>();
            var customer = _mapperMock.Map<AddCustomerDto, Customer>(request);
            _serviceMock.Setup(x => x.Customer.Add(customer)).Verifiable();
            request.Password = null;

            // Act
            var result = _controller.Add(request);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void AddCustomer_Should_Return_BadRequest_For_Validate_PhoneNumber()
        {
            // Arrange
            var request = _fixture.Create<AddCustomerDto>();
            var customer = _mapperMock.Map<AddCustomerDto, Customer>(request);
            _serviceMock.Setup(x => x.Customer.Add(customer)).Verifiable();
            request.PhoneNumber = null;

            // Act
            var result = _controller.Add(request);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}