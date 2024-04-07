using AutoFixture;
using AutoMapper;
using CustomerService.Application.Interface;
using CustomerService.Controllers.CustomerController;
using CustomerService.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CustomerService.Test.Controllers;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using CustomerService.Application.Dto.Customer;


namespace CustomerService.Test.Controllers.CustomerController
{

    public class CustomerLoginControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly CustomerLoginController _controller;
        private readonly IMapper _mapperMock;

        public CustomerLoginControllerTests() {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();

            var mapperMock = new Mock<IMapper>();
            _mapperMock = mapperMock.Object;

            mapperMock.Setup(m => m.Map<CustomerLoginDto, Customer>(It.IsAny<CustomerLoginDto>()))
                .Returns((CustomerLoginDto source) => new Customer());

            _mapperMock = mapperMock.Object;

            _controller = new CustomerLoginController(_serviceMock.Object);


        }
       
        [Fact]
        public void CustomerLogin_Should_Return_BadRequest_For_NotNull_EmailOrName()
        {
            // Arrange
            var request = _fixture.Create<CustomerLoginDto>();
            request.EmailOrName = "example@example.com"; // Set a non-empty value for EmailOrName
            request.Password = "password123"; // Set a non-empty value for Password

            // Act
            var result = _controller.Login(request);

            // Assert
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);

            // Additional assertion for GUID/UUID
            var value = objectResult.Value;
            Assert.NotNull(value);
            Assert.IsType<string>(value);

            string stringValue = (string)value;
            Assert.False(string.IsNullOrEmpty(stringValue));


        }
      

        [Fact]
        public void Password_Should_Contain_Numbers_Letters_Symbols()
        {
            // Arrange
            var request = _fixture.Create<CustomerLoginDto>();
            request.Password = "Abcd123!";// Set a password containing numbers, letters, and symbols (the same the rule)


            // Act
            var containsNumbers = Regex.IsMatch(request.Password, @"\d"); // Check for numbers
            var containsLetters = Regex.IsMatch(request.Password, @"[a-zA-Z]"); // Check for letters
            var containsSymbols = Regex.IsMatch(request.Password, @"[^a-zA-Z0-9]"); // Check for symbols
            var result = _controller.Login(request);
            // Assert
            Assert.True(containsNumbers);
            Assert.True(containsLetters);
            Assert.True(containsSymbols);
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}
