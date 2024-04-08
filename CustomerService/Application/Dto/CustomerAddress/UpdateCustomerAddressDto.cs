using System.ComponentModel.DataAnnotations;

namespace CustomerService.Application.Dto.CustomerAddress
{
    public class UpdateCustomerAddressDto
    {       
        private string _address;
        private float _geoLat;
        private float _geoLon;
     
        public string Address { get => _address; set => _address = value; }
        public float GeoLat { get => _geoLat; set => _geoLat = value; }
        public float GeoLon { get => _geoLon; set => _geoLon = value; }
    }
}
