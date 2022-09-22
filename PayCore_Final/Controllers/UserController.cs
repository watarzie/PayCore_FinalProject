using AutoMapper;
using Base.Response;
using Base.Token;
using Dto;
using EmailService.Domain;
using EmailService.Services;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayCore_Final.Extensions;
using Service.Token.Abstract;
using Service.UserRegister.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PayCore_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRegisterService userRegisterService;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;
        private readonly IBackgroundJobClient backgroundJobClient;
        public UserController(IUserRegisterService userRegisterService, ITokenService tokenService, IMapper mapper, IEmailSender emailSender, IBackgroundJobClient backgroundJobClient)
        {
            this.userRegisterService = userRegisterService;
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.emailSender = emailSender;
            this.backgroundJobClient = backgroundJobClient;
            
        }
        [HttpPost("Register")]
        public BaseResponse<UserRegisterDto> Register([FromBody] UserRegisterDto dto)
        {
            var Hash = MD5Extension.MD5Hash(dto.Password);
            dto.Password = Hash;
            var response = userRegisterService.Insert(dto);
            if(response.Success)
            {
                var message = new Message(dto.Email, "PayCore Hoşgeldiniz!", "Üyeliğiniz gerçekleştirilmiştir");
                emailSender.SendEmailAsync(message);
                backgroundJobClient.Enqueue<IEmailSender>(x => x.SendEmailAsync(message));
            }
           
            return response;
        }
        [HttpPost("Login")]
        public BaseResponse<TokenResponse> Increment([FromBody] TokenRequest request)
        {
            var Hash = MD5Extension.MD5Hash(request.Password);
            request.Password = Hash;
            var response = tokenService.GenerateToken(request);
            if(response.Success) // işlem eğer başarılı olursa giren kullanıcıya hangfire ile mail atılır
            {
                var message = new Message(request.Email, "PayCore Hoşgeldiniz!", "Giriş Başarılı!");
                emailSender.SendEmailAsync(message);
                backgroundJobClient.Enqueue<IEmailSender>(x => x.SendEmailAsync(message));
            }
            return response;
        }
    }
}
