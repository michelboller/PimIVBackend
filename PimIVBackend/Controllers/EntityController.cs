using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PimIVBackend.Controllers.Base;
using PimIVBackend.Models;
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
    public class EntityController : ApiControllerBase
    {
        private readonly IEntityGuestServices _entityGuestServices;
        private readonly AppDbContext _context;
        public EntityController(IEntityGuestServices entityGuestServices, AppDbContext context)
        {
            _entityGuestServices = entityGuestServices;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<EntityGuest>>> Get()
        {
            return await _context.Entities.OfType<EntityGuest>().ToListAsync();
        }

        //TODO: Arrumar o model de entrada para ter os campos certos para todos os metodos que recebem um EntutyGuest como entrada
        [HttpPost]
        public async Task<IActionResult> CreateAsync(EntityGuest model)
        {
            try
            {
                await _entityGuestServices.CreateAsync(model);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowException(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(EntityGuest entity)
        {
            try
            {
                await _entityGuestServices.UpdateAsync(entity);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowException(ex);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> ActivateInactivate(int id)
        {
            try
            {
                await _entityGuestServices.ActivateInactivate(id);
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
