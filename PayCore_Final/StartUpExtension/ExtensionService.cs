using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Service.Mapper;
using Service.Token.Abstract;
using Service.Token.Concrete;
using Service.UserRegister.Abstract;
using Service.UserRegister.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore_Final.StartUpExtension
{
    public static  class ExtensionService
    {
        public static void AddServices(this IServiceCollection services)
        {
            // services 
            services.AddScoped<IUserRegisterService, UserRegisterService>();
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
