﻿using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Entities.Service.Contract;
using Talabat.Core.Repositories.Contract;
using Talabat.Errors;
using Talabat.Helpers;
using Talabat.Repository;
using Talabat.Service.AuthServices;
using Talabat.Service.OrderServices;
using Talabat.Service.ProductService;

namespace Talabat.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection  AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IProductServices), typeof(ProductServices));
            services.AddScoped(typeof(IOrderService), typeof(OrderServices ));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IBasketRepository ), typeof(BasketRepository ));
            services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddAutoMapper(typeof(MappingProfiles));
           services.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = (ActionContext) => {
                    var error = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0).SelectMany(p => p.Value.Errors).Select(E => E.ErrorMessage).ToList();
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }
    }
}
