

using AutoFixture;
using AutoMapper;
using CustomerService.Application.Dto.CustomerAddress22;
using CustomerService.Application.Dto.Common;
using CustomerService.Application.Interface;
using CustomerService.Controllers.CustomerAddressController;
using CustomerService.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using CustomerService.Application.Dto.Customer;
using CustomerService.Controllers.CustomerController;

namespace CustomerService.Test.Controllers.CustomerAddressController
{
    public class AddCustomerAddressControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly AddCustomerAddressController _controllerAddCustomerAddress;

        private readonly IMapper _mapperMock;

        public AddCustomerAddressControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();

            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<AddCustomerDto, Customer>(It.IsAny<AddCustomerDto>()))
             .Returns((AddCustomerDto source) => new Customer());

            _mapperMock = mapperMock.Object;

            _controllerAddCustomerAddress = new AddCustomerAddressController(_serviceMock.Object);
        }

        [Fact]
        public void AddCustomerAddress_ShouldRetunGuid()
        {
            // Arrange         
            var requestCustomerAddress = _fixture.Create<AddCustomerAddressDto>();          
            var customerAddress = _mapperMock.Map<AddCustomerAddressDto, CustomerAddress>(requestCustomerAddress);
            _serviceMock.Setup(x => x.CustomerAddress.Add(customerAddress)).Verifiable();

            // Act         
            var resultCustomerAddress = _controllerAddCustomerAddress.Add(requestCustomerAddress);

            // Assert                       
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(resultCustomerAddress.Result);
            bool isValidGuid = Guid.TryParse(((ReturnGuidDto)objectResult.Value).Id, out Guid guidResult);
            Assert.True(isValidGuid);
        }                             
    }
}