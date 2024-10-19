using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Service.Contract;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications.Order_Specs;

namespace Talabat.Service.OrderServices
{
    public class OrderServices : IOrderService
    {
        private readonly IBasketRepository  _basketRepo;
        private readonly IUnitOfWork _unitofwork;

        public OrderServices(IBasketRepository basketRepo, IUnitOfWork  unitofwork
            )
        {
            _basketRepo = basketRepo;
            _unitofwork = unitofwork;
            
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string BasketId, Address shippingAddress, int deliveryMethodId)
        {

            var basket = await _basketRepo.GetBasketAsync(BasketId);
            var orderItems = new List<OrderItem>();
            if (basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitofwork.Repositry<Product>().GetAsync(item.Id);
                    var ProductOrdered = new ProductItemOrdered (product.Id, product.Name, product.PictureUrl);
                    var orderItem = new OrderItem(ProductOrdered, product.Price, item.Quantity  );
                    orderItems.Add(orderItem);
                }
            }

            var Subtotal = orderItems.Sum(item => item.Price * item.Quantity );

            var deliveryMethods = await _unitofwork.Repositry<DeliveryMethod>().GetAsync(deliveryMethodId);
            var orderRepo = _unitofwork.Repositry<Order>();
            //var spec = new OrderSpecifications (basket?.);
            //var existingOrder = await orderRepo.GetWithSpecAsync(spec);
            //if (existingOrder != null)
            //{
            //    orderRepo.Delete(existingOrder);
            //    //await _paymentServices.CreateOrUpdatePaymentIntent(BasketId);
            //}


            var order = new Order(
                buyerEmail: buyerEmail ,
              shippingAddress: shippingAddress,
               deliverymethod: deliveryMethods,
                items: orderItems,
                subtotal: Subtotal
                //paymentIntentId: basket?.PaymentIntentId ?? ""

                );


            _unitofwork.Repositry<Order>().Add(order);
            var result = await _unitofwork.CompleteAsync();
            if (result <= 0) return null;
            return order;
         
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order?> GetOrderByIdForUserAsync(int OrderId, string BuyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
