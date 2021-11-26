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
    public class ReservationController : ApiControllerBase
    {
        private readonly IReservationServices _reservationService;
        private readonly AppDbContext _context;

        public ReservationController(IReservationServices reservationService, AppDbContext context)
        {
            _reservationService = reservationService;
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Reservation>>> GetReservations()
        {
            return await _context.Reservations
                .Include(x => x.Guests)
                .Include(x => x.MainGuest)
                .Include(x => x.EntityCompany)
                .Include(x => x.Room)
                .ToListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Reservation>> GetReservationById(int id)
        {
            return await _context.Reservations
                .Include(x => x.Guests)
                .Include(x => x.MainGuest)
                .Include(x => x.EntityCompany)
                .Include(x => x.Room)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateReservation(ReservationCreateDto reservation)
        {
            try
            {
                await _reservationService.Create(reservation);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return ThrowException(e);
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateReservation(ReservationUpdateDto reservation)
        {
            try
            {
                await _reservationService.Update(reservation);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return ThrowException(e);
            }
        }

        [HttpPatch]
        [Route("AddGuests/{reservationId}/{guestId}")]
        public async Task<IActionResult> AddGuests(int reservationId, int guestId)
        {
            try
            {
                await _reservationService.AddGuest(reservationId, guestId);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return ThrowException(e);
            }
        }

        [HttpPatch]
        [Route("RemoveGuests/{reservationId}/{guestId}")]
        public async Task<IActionResult> RemoveGuests(int reservationId, int guestId)
        {
            try
            {
                await _reservationService.RemoveGuest(reservationId, guestId);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return ThrowException(e);
            }
        }

        [HttpPatch]
        [Route("ChangeRoom/{reservationId}/{roomId}")]
        public async Task<IActionResult> ChangeRoom(int reservationId, int roomId)
        {
            try
            {
                await _reservationService.ChangeRoom(reservationId, roomId);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return ThrowException(e);
            }
        }

        [HttpPatch]
        [Route("AddCompany/{reservationId}/{companyId}")]
        public async Task<IActionResult> AddCompany(int reservationId, int companyId)
        {
            try
            {
                await _reservationService.AddCompany(reservationId, companyId);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception e)
            {
                return ThrowException(e);
            }
        }

        [HttpPatch]
        [Route("ChangeCompany/{reservationId}/{companyId}")]
        public async Task<IActionResult> ChangeCompany(int reservationId, int companyId)
        {
            try
            {
                await _reservationService.ChangeCompany(reservationId, companyId);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return ThrowException(e);
            }
        }

        [HttpPatch]
        [Route("RemoveCompany/{reservationId}")]
        public async Task<IActionResult> RemoveCompany(int reservationId)
        {
            try
            {
                await _reservationService.RemoveCompany(reservationId);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return ThrowException(e);
            }
        }
    }
}
