using AutoMapper;
using AutoMapper.Execution;
using Talabat.Core.Entities;
using Talabat.DTOs;

namespace Talabat.Helpers
{
    public class PictureUrlResolver:IValueResolver<Product,ProductToReturnDto ,string>
    {
        private readonly IConfiguration _configration;

        public PictureUrlResolver(IConfiguration configration)
        {
            _configration = configration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            
                return $"{_configration["ApiBaseUrl"]}/{source .PictureUrl }";

            return string.Empty;

          
        }
    }
}
