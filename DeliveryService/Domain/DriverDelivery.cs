using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Domain
{
    [Table("DriverDelivery", Schema = "delivery")]
    public class DriverDelivery
    {
        private string _id;
        private List<Driver> _driver;
        private List<DeliveryRequest> _deliveryRequest;
        private string _deliverStatus;
        private DateTime _createTime;
        private DateTime _onwayTime;
        private DateTime _deliveredTime;

        public string Id { get => _id; set => _id = value; }
        public List<Driver> Driver { get => _driver; set => _driver = value; }
        public List<DeliveryRequest> DeliveryRequest { get => _deliveryRequest; set => _deliveryRequest = value; }
        public string DeliverStatus { get => _deliverStatus; set => _deliverStatus = value; }
        public DateTime CreateTime { get => _createTime; set => _createTime = value; }
        public DateTime OnwayTime { get => _onwayTime; set => _onwayTime = value; }
        public DateTime DeliveredTime { get => _deliveredTime; set => _deliveredTime = value; }
    }
}
