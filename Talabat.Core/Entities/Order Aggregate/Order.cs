using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public  class Order:BaseEntity 
    {
        public string BuyerEmail { get; set; } = null!;
        public DateTimeOffset  OrderDate { get; set;}= DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.pending;
        public Address shippingAddress { get; set; } = null!;
        //public int DeliveryMethodId{ get; set; }
        public DeliveryMethod DeliveryMethod { get; set; } = null!;//nav property
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public  decimal Subtotal { get; set; }
        public decimal GetTotal() => Subtotal + DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; }


    }
}
