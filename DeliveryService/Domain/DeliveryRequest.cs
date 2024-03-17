using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Domain
{
    [Table("DeliveryRequest", Schema = "delivery")]
    public class DeliveryRequest
    {
        private string _id;
        private string _orderId;     
        private string _status;
        private DateTime _createTime;
        private DateTime _updateTime;

        public string Id { get => _id; set => _id = value; }
        public string OrderId { get => _orderId; set => _orderId = value; }
        public string Status { get => _status; set => _status = value; }
        public DateTime CreateTime { get => _createTime; set => _createTime = value; }
        public DateTime UpdateTime { get => _updateTime; set => _updateTime = value; }
    }
}
