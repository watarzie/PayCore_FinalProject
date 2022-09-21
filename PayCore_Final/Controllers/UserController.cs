using AutoMapper;
using Base.Response;
using Base.Token;
using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayCore_Final.Extensions;
using Service.Token.Abstract;
using Service.UserRegister.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public UserController(IUserRegisterService userRegisterService, ITokenService tokenService, IMapper mapper)
        {
            this.userRegisterService = userRegisterService;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }
        [HttpPost("Register")]
        public BaseResponse<UserRegisterDto> Register([FromBody] UserRegisterDto dto)
        {
            var Hash = MD5Extension.MD5Hash(dto.Password);
            dto.Password = Hash;
            var response = userRegisterService.Insert(dto);
            return response;
        }
        [HttpPost("Login")]
        public BaseResponse<TokenResponse> Increment([FromBody] TokenRequest request)
        {
            var Hash = MD5Extension.MD5Hash(request.Password);
            request.Password = Hash;
            var response = tokenService.GenerateToken(request);
            return response;
        }
    }
}
