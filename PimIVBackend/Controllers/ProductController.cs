using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PimIVBackend.Controllers.Base;
using PimIVBackend.Models;
using PimIVBackend.Models.CreateDto;
using PimIVBackend.Models.UpdateDto;
using PimIVBackend.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PimIVBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly AppDbContext _context;
        public ProductController(IProductServices productServices, AppDbContext context)
        {
            _productServices = productServices;
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateProduct(ProductCreateDto model)
        {
            try
            {
                await _productServices.Create(model);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowException(ex);
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto model)
        {
            try
            {
                await _productServices.Update(model);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowException(ex);
            }
        }

        [HttpPatch]
        [Route("")]
        public async Task<IActionResult> ActivateDeactivate(int id)
        {
            try
            {
                await _productServices.ActivateInactivate(id);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowException(ex);
            }
        }
    }
}
