using System.ComponentModel;
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

        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Email { get => _email; set => _email = value; }
        public string Password { get => _password; set => _password = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }     
        public DateTime CreateTime { get => _createTime; set => _createTime = value; }
        public DateTime UpdateTime { get => _updateTime; set => _updateTime = value; }
        public List<CustomerAddress> CustomerAddresses { get => _customerAddresses; set => _customerAddresses = value; }
    }
}