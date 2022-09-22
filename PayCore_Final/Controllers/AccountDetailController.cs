using AutoMapper;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayCore_Final.ServiceOffer;
using PayCore_Final.ServiceProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PayCore_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountDetailController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IOfferService offerService;
        private readonly IMapper mapper;
        public AccountDetailController(IProductService productService, IOfferService offerService, IMapper mapper)
        {
            this.productService = productService;
            this.offerService = offerService;
            this.mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetOffers()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int userid= int.Parse(userId);
            var response = offerService.GetAllByOffers(userid); // Yaptıgı offerları getirir
            return Ok(response);
            
        }
        [Authorize]
        [HttpGet("Offers")]
        public IActionResult GetOfferMyProduct() // Kullanıcının ürünlerine aldığı offerları döner
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            int userid = int.Parse(userId);
            var response = productService.GetAllProduct(userid); 
            foreach (var item in response)
            {
                var control = offerService.GetAllByOffersProduct(item.Id);

                return Ok(control);
            }
            return BadRequest();

        }
      

    }
}
