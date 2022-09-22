using AutoMapper;
using Data.Model;
using Microsoft.Extensions.DependencyInjection;
using Paycore_Final.Service;
using Paycore_Final.UpdateProduct;
using PayCore_Final.ServiceOffer;
using PayCore_Final.ServiceProduct;
using PayCore_Final.UpdateProduct;
using Service.Mapper;
using Service.Token.Abstract;
using Service.Token.Concrete;
using Service.UserRegister.Abstract;
using Service.UserRegister.Concrete;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace PayCore_Final.StartUpExtension
{
    public static  class ExtensionService
    {
        public static void AddServices(this IServiceCollection services)
        {
            // services 
            services.AddScoped<IUserRegisterService, UserRegisterService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IUpdateProductService, UpdateProductService>();
            services.AddScoped<ITokenService, TokenService>();

            // mapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}
