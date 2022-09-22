using AutoMapper;
using Data.Model;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paycore_Final.UpdateProduct;
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
    public class OfferController : ControllerBase
    {
        private readonly IOfferService offerService;
        private readonly IProductService productService;
        private readonly IUpdateProductService updateProductService;
        private readonly IMapper mapper;
        public OfferController(IOfferService offerService,IProductService productService, IMapper mapper,IUpdateProductService updateProductService)
        {
            this.offerService = offerService;
            this.mapper = mapper;
            this.productService = productService;
            this.updateProductService = updateProductService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = offerService.GetAll();
            return Ok(response);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] OfferDto dto)
        {
            
            var entity = productService.GetProduct(dto.ProductId);
            if(entity.IsOfferable==false || entity.IsSold==true)
            {
                return BadRequest("Teklif Verilemez");
            }
            var userId= (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            dto.UserId = int.Parse(userId);
            var response = offerService.Insert(dto);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            if (response.Response is null)
            {
                return NoContent();
            }
            if (response.Success)
            {
                return StatusCode(201, response);
            }
            return BadRequest(response);

        }

        [Authorize]
        [HttpPost("BuyProductWithoutOffer")]
        public IActionResult Buy(int productId,[FromBody] UpdateProductDto dto)
        {
            if(dto.IsSold==true)
            {
                var response = updateProductService.Update(productId, dto);
                return Ok(response);
            }
            return BadRequest();
            
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody]OfferDto dto)
        {
            var control = offerService.GetOffer(id);
            if(control is null)
            {
                return BadRequest("Böyle bir teklif bulunmamaktadır");
            }
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            dto.UserId = int.Parse(userId);
            if (control.UserId == dto.UserId)
            {
                var response = offerService.Update(id, dto);
                return Ok(response);
            }
            return Unauthorized();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var control = offerService.GetOffer(id);
            if(control is null)
            {
                return BadRequest("Böyle bir teklif bulunmamaktadır");
            }
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            if (control.UserId == int.Parse(userId))
            {
                var response = offerService.Remove(id);
                return Ok(response);
            }
            return Unauthorized();
        }
    
    }
}
