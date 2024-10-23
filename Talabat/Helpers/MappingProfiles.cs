using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
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
            CreateMap<AddressDto, Address>();
            CreateMap<Order, OrderToReturnDto >().ForMember(d=>d.DeliveryMethod,O=>O.MapFrom(s=>s.DeliveryMethod.ShortName)).
                ForMember(d => d.DeliveryMethodCost, O => O.MapFrom(s => s.DeliveryMethod.Cost))
                ;
            CreateMap<OrderItem, OrderItemDto>().ForMember(d => d.ProductName, O => O.MapFrom(s => s.Product.ProductName)).
                ForMember(d => d.ProductId, O => O.MapFrom(s => s.Product.ProductId)).
                ForMember(d => d.PictureUrl, O => O.MapFrom(s => s.Product.PictureUrl )).
                  ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>()).
                      ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());
        }

    }
}
