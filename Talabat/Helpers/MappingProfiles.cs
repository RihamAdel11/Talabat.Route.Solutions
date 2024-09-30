using AutoMapper;
using Talabat.Core.Entities;
using Talabat.DTOs;

namespace Talabat.Helpers
{
    public class MappingProfiles:Profile
    {
       

        public MappingProfiles()
        {
           
            CreateMap <Product ,ProductToReturnDto > ().ForMember (d=>d.Brand ,o=>o.MapFrom (s=>s.Brand .Name )).ForMember (
                d=>d.Category,o=>o.MapFrom (s=>s.Category .Name )).ForMember (
                d=>d.PictureUrl ,o=>o.MapFrom<PictureUrlResolver >());
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }

    }
}
