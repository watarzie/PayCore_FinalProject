using AutoMapper;
using Base.Response;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paycore_Final.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public BaseResponse<IEnumerable<CategoryDto>> GetAll()
        {
            var response = categoryService.GetAll();
            return mapper.Map<BaseResponse<IEnumerable<CategoryDto>>>(response);
        }

        [Authorize]
        [HttpPost]
        public BaseResponse<CategoryDto> Create([FromBody] CategoryDto dto)
        {
            var response = categoryService.Insert(dto);
            return response;
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] CategoryDto dto)
        {
            var response = categoryService.Update(id, dto);
            return Ok(response);
        }
        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var response = categoryService.Remove(id);
            return Ok(response);
        }
    }
}
