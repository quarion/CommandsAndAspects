using System.Collections.Generic;

namespace CommandsAndAspects.Domain
{
    public class Order
    {
        public string Id { get; protected set; }

        public string DeliveryAddres { get; protected set; }

        public IEnumerable<string> OrderItemIds { get; protected set; }

        public string Status { get; protected set; }

        private Order() {
        }

        public static Order CreateNew(string id, string deliveryAddres, IEnumerable<string> orderItemIds)
        {
            var newOrder = new Order()
            {
                Id = id,
                DeliveryAddres = deliveryAddres,
                OrderItemIds = orderItemIds,
                Status = "Created"
            };


            return newOrder;
        }

        public void Void()
        {
            this.Status = "Voided";
        }
    }
}