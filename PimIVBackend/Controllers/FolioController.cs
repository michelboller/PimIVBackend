using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PimIVBackend.Controllers.Base;
using PimIVBackend.Models;
using PimIVBackend.Models.CreateDto;
using PimIVBackend.Models.Dto;
using PimIVBackend.Models.UpdateDto;
using PimIVBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimIVBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FolioController : ApiControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IFolioServices _folioServices;
        public FolioController(AppDbContext context, IFolioServices folioServices)
        {
            _context = context;
            _folioServices = folioServices;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Folio>>> GetFolios()
        {
            var a = await _context.Folios
                .Include(x => x.FolioItems)
                .Include(x => x.FolioItems).ThenInclude(x => x.Product)
                .Include(x => x.Entities).ThenInclude(x => x.Entity)
                .Include(x => x.Reservation).ThenInclude(X => X.EntityCompany)
                .Include(x => x.Reservation).ThenInclude(X => X.Room)
                .ToListAsync();
            return a;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Folio>> GetFolioById(int id)
        {
            return await _context.Folios.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        [Route("PostProduct")]
        public async Task<IActionResult> PostFolioItem(PostProductDto model)
        {
            try
            {
                await _folioServices.PostProduct(model.FolioId, model.ProductId, model.GuestId, model.Quantity);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return ThrowException(e);
            }
        }

        [HttpPut]
        [Route("RemoveProduct")]
        public async Task<IActionResult> RemoveProduct(RemoveFolioItemDto model)
        {
            try
            {
                await _folioServices.RemoveProduct(model.FolioId, model.FolioItemId);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return ThrowException(e);
            }
        }

        [HttpGet]
        [Route("DoCheckOut/{folioId}")]
        public async Task<ActionResult<ClosedFolioDto>> DoCheckOut(int folioId)
        {
            var response = await _folioServices.DoCheckOut(folioId);
            await _context.SaveChangesAsync();
            return Ok(response);
        }

    }
}
