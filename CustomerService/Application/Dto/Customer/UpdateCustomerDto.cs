using System.ComponentModel.DataAnnotations;

namespace CustomerService.Application.Dto.Customer
{
    public class UpdateCustomerAddressDto
    {
        private string _name;
        private string _email;
        private string _password;
        private string _phoneNumber;

        public string Name { get => _name; set => _name = value; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
    }
}
