namespace CustomerService.Application.Dto.CustomerAddress
{
    public class ViewCustomerAddressDto
    {
        private string _id;
        private string _customerId;
        private string _address;
        private string _phoneNumber;
        private float _geoLat;
        private float _geoLon;
        private DateTime _createTime;
        private DateTime _updateTime;

        public string Id { get => _id; set => _id = value; }
        public string CustomerId { get => _customerId; set => _customerId = value; }
        public string Address { get => _address; set => _address = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public float GeoLat { get => _geoLat; set => _geoLat = value; }
        public float GeoLon { get => _geoLon; set => _geoLon = value; }
        public DateTime CreateTime { get => _createTime; set => _createTime = value; }
        public DateTime UpdateTime { get => _updateTime; set => _updateTime = value; }
    }
}
