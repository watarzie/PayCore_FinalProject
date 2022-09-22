using AutoMapper;
using Base.Response;
using Data.Model;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet("{categoryId}")]
        public IActionResult GetByCategory(int categoryId)
        {
            IEnumerable<Product> productDto = productService.GetAllByCategoryId(categoryId);
            return Ok(productDto);

        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] ProductDto dto)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            dto.UserId = int.Parse(userId);
            var response = productService.Insert(dto);
            if(!response.Success)
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
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductDto dto)
        {
            var control = productService.GetProduct(id);
            if(control is null)
            {
                return BadRequest("Böyle bir ürün bulunmamaktadır");
            }
            var userId= (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            dto.UserId= int.Parse(userId);
            if(control.UserId==dto.UserId)
            {
                var response = productService.Update(id, dto);
                return Ok(response);
            }
            return Unauthorized();
            
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var control = productService.GetProduct(id);
            if (control is null)
            {
                return BadRequest("Böyle bir ürün bulunmamaktadır");
            }
            var userId = (User.Identity as ClaimsIdentity).FindFirst("UserId").Value;
            
            if (control.UserId == int.Parse(userId))
            {
                var response = productService.Remove(id);
                return Ok(response);
            }
            return Unauthorized();

        }
    }
}
