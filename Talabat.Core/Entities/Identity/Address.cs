﻿namespace Talabat.Core.Entities.Identity
{
    public class Address:BaseEntity
    {
       
        public string FristName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public string ApplicationUserId { get; set; }



    }
}