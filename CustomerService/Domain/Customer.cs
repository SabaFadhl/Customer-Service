using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerService.Domain
{
    [Table("Customer", Schema = "customer")]
    public class Customer
    {
        private string _id;
        private string _name;
        private string _email;
        private string _password;
        private string _phoneNumber;
        private DateTime _createTime;
        private DateTime _updateTime;

        private List<CustomerAddress> _customerAddresses;

        [Key]
        public string Id { get => _id; set => _id = value; }

        [MaxLength(150, ErrorMessage = "This field must be less than or equals 150 character")]
        public string Name { get => _name; set => _name = value; }
       
        [Required]
        [MaxLength(150, ErrorMessage = "This field must be less than or equals 150 character")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get => _email; set => _email = value; }

        [MaxLength(500, ErrorMessage = "This field must be less than or equals 150 character")]
        public string Password { get => _password; set => _password = value; }

        [MaxLength(50, ErrorMessage = "This field must be less than or equals 150 character")]
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }         
        public DateTime CreateTime { get => _createTime; set => _createTime = value; }
        public DateTime UpdateTime { get => _updateTime; set => _updateTime = value; }
        public List<CustomerAddress> CustomerAddresses { get => _customerAddresses; set => _customerAddresses = value; }
    }
}