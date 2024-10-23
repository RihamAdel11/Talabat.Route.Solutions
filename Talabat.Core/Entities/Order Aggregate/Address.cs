﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Address
    {
        public Address()
        {
            
        }
        public Address(string fristName,string lastName,string street,string city,string country)
        {
            FristName=fristName;
            LastName=lastName;
            Street=street;
            City=city;
                Country=country;


       }
        
        public required string FristName { get; set; } 
        public string LastName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
