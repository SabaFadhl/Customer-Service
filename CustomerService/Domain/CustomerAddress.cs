using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerService.Domain
{
    [Table("CustomerAddresses", Schema = "customer")]
    public class CustomerAddress
    {
        private string _id;
        private string _customerId;
        private string _address;
        private float _geoLat;
        private float _geoLon;  
        private DateTime _createTime;
        private DateTime _updateTime;

        public string Id { get => _id; set => _id = value; }
        [MaxLength(500, ErrorMessage = "This field must be less than or equals 150 character")]
        public string Address { get => _address; set => _address = value; }       
        public float GeoLat { get => _geoLat; set => _geoLat = value; }
        public float GeoLon { get => _geoLon; set => _geoLon = value; }
        public DateTime CreateTime { get => _createTime; set => _createTime = value; }
        public DateTime UpdateTime { get => _updateTime; set => _updateTime = value; }
    }
}
