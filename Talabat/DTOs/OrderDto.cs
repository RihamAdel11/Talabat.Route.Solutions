﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.DTOs
{
    public class OrderDto
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    
    }
}
