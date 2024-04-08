namespace CustomerService.Application.Dto.Customer
{
    public class ViewCustomerDto
    {
        private string _id;
        private string _name;
        private string _email;
        private string _phoneNumber;
        private DateTime _createTime;
        private DateTime _updateTime;

        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Email { get => _email; set => _email = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public DateTime CreateTime { get => _createTime; set => _createTime = value; }
        public DateTime UpdateTime { get => _updateTime; set => _updateTime = value; }
    }
}
