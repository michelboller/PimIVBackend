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
    public class RoomController : ApiControllerBase
    {
        private readonly IRoomServices _roomServices;
        private readonly AppDbContext _context;
        public RoomController(IRoomServices roomServices, AppDbContext context)
        {
            _roomServices = roomServices;
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Room>>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Room>> GetRoomById(int id)
        {
            return await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateRoom(RoomCreateDto model)
        {
            try
            {
                await _roomServices.Create(model);
                await _context.SaveChangesAsync();
                return Ok();

            } catch (Exception ex)
            {
                return ThrowException(ex);
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateRoom(RoomUpdateDto model)
        {
            try
            {
                await _roomServices.Update(model);
                await _context.SaveChangesAsync();
                return Ok();
            } catch (Exception ex)
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
                await _roomServices.ActivateInactivate(id);
                await _context.SaveChangesAsync();
                return Ok();
            } catch (Exception ex)
            {
                return ThrowException(ex);
            }
        }
    }
}
