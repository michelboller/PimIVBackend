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
        private readonly IEntityCompanyServices _entityCompanyServices;
        private readonly AppDbContext _context;
        public EntityController(IEntityGuestServices entityGuestServices, AppDbContext context, IEntityCompanyServices entityCompanyServices)
        {
            _entityGuestServices = entityGuestServices;
            _entityCompanyServices = entityCompanyServices;
            _context = context;
        }

        [HttpGet]
        [Route("Guest")]
        public async Task<ActionResult<List<EntityGuest>>> GetGuests()
        {
            return await _context.Entities.OfType<EntityGuest>().ToListAsync();
        }

        [HttpGet]
        [Route("Guest/{id}")]
        public async Task<ActionResult<EntityGuest>> GetGuestById(int id)
        {
            return await _context.Entities.OfType<EntityGuest>().FirstOrDefaultAsync(x => x.Id == id);
        }

        //TODO: Arrumar o model de entrada para ter os campos certos para todos os metodos que recebem um EntutyGuest como entrada
        [HttpPost]
        [Route("Guest")]
        public async Task<IActionResult> CreateGuestAsync(EntityGuestCreateDto model)
        {
            try
            {
                await _entityGuestServices.CreateAsync(new EntityGuest(model.Name, model.Address, model.CEP, model.Phone, model.Document, model.DocType, true, model.Gender));
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowException(ex);
            }
        }

        [HttpPut]
        [Route("Guest")]
        public async Task<IActionResult> UpdateGuestAsync(EntityGuestUpdateDto model)
        {
            try
            {
                await _entityGuestServices.UpdateAsync(model);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowException(ex);
            }
        }

        [HttpPatch]
        [Route("Guest")]
        public async Task<IActionResult> ActivateInactivateGuest(int id)
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

        [HttpGet]
        [Route("Company")]
        public async Task<ActionResult<List<EntityCompany>>> GetCompanies()
        {
            return await _context.Entities.OfType<EntityCompany>().ToListAsync();
        }

        [HttpGet]
        [Route("Company/{id}")]
        public async Task<ActionResult<EntityCompany>> GetCompanyById(int id)
        {
            return await _context.Entities.OfType<EntityCompany>().FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        [Route("Company")]
        public async Task<IActionResult> CreateCompanyAsync(EntityCompanyCreateDto company)
        {
            try
            {
                await _entityCompanyServices.CreateAsync(new EntityCompany(company.Name, company.Address, company.CEP, company.Phone, company.Document, true));
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowException(ex);
            }
        }

        [HttpPut]
        [Route("Company")]
        public async Task<IActionResult> UpdateCompanyAsync(EntityCompanyUpdateDto company)
        {
            try
            {
                await _entityCompanyServices.UpdateAsync(company);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return ThrowException(ex);
            }
        }

        [HttpPatch]
        [Route("Company")]
        public async Task<IActionResult> ActivateInactivateCompany(int id)
        {
            try
            {
                await _entityCompanyServices.ActivateInactivate(id);
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
