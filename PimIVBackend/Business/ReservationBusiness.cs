using Microsoft.EntityFrameworkCore;
using PimIVBackend.Models;
using PimIVBackend.Models.CreateDto;
using PimIVBackend.Models.UpdateDto;
using PimIVBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Validator;

namespace PimIVBackend.Business
{
    public class ReservationBusiness : IReservationServices
    {
        private readonly AppDbContext _context;

        public ReservationBusiness(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCompany(int reservationId, int companyId)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido")
                    .IsGratterThanZeroAndPositive(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido")
                    .NotDefault(companyId, nameof(companyId), $"{nameof(companyId)} está com um valor inválido")
                    .IsGratterThanZeroAndPositive(companyId, nameof(companyId), $"{nameof(companyId)} está com um valor inválido"));

            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == reservationId);
            var company = await _context.Entities.OfType<EntityCompany>().FirstOrDefaultAsync(x => x.Id == companyId);

            if(reservation != null)
            {
                if (company != null && reservation.EntityCompanyId == null && reservation.EntityCompany == null)
                    reservation.AddCompany(company);
            }
        }

        public async Task AddGuest(int reservationId, int guestId)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido")
                    .IsGratterThanZeroAndPositive(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido")
                    .NotDefault(guestId, nameof(guestId), $"{nameof(guestId)} está com um valor inválido")
                    .IsGratterThanZeroAndPositive(guestId, nameof(guestId), $"{nameof(guestId)} está com um valor inválido"));

            var reservation = await _context.Reservations.Include(x => x.Guests).FirstOrDefaultAsync(x => x.Id == reservationId);
            var guest = await _context.Entities.OfType<EntityGuest>().FirstOrDefaultAsync(x => x.Id == guestId);

            if (reservation != null)
            {
                if (guest != null && !reservation.Guests.Contains(guest))
                    reservation.AddGuests(guest);
            }
        }

        public async Task ChangeCompany(int reservationId, int companyId)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido")
                    .IsGratterThanZeroAndPositive(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido")
                    .NotDefault(companyId, nameof(companyId), $"{nameof(companyId)} está com um valor inválido")
                    .IsGratterThanZeroAndPositive(companyId, nameof(companyId), $"{nameof(companyId)} está com um valor inválido"));

            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == reservationId);
            var company = await _context.Entities.OfType<EntityCompany>().FirstOrDefaultAsync(x => x.Id == companyId);

            if (reservation != null)
            {
                if (company != null && reservation.EntityCompanyId != company.Id)
                    reservation.ChangeCompany(company);
            }
        }

        public async Task ChangeRoom(int reservationId, int roomId)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido")
                    .IsGratterThanZeroAndPositive(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido")
                    .NotDefault(roomId, nameof(roomId), $"{nameof(roomId)} está com um valor inválido")
                    .IsGratterThanZeroAndPositive(roomId, nameof(roomId), $"{nameof(roomId)} está com um valor inválido"));

            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == reservationId);
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == roomId);

            if (reservation != null)
            {
                if (room != null && reservation.RoomId != roomId)
                    reservation.ChangeRoom(room);
            }
        }

        public async Task Create(ReservationCreateDto model)
        {
            Guard.Validate(validator =>
            validator
                .NotNull(model, nameof(model), $"{nameof(model)} é uma referência de um objeto nulo"));

            var mainGuest = await _context.Entities.OfType<EntityGuest>().FirstOrDefaultAsync(x => x.Id == model.GuestId);
            var company = default(EntityCompany);
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == model.RoomId);

            if (model.CompanyId != null && model.CompanyId != 0)
                company = await _context.Entities.OfType<EntityCompany>().FirstOrDefaultAsync(x => x.Id == model.CompanyId);

            if (mainGuest != null && company == null && room != null)
                await _context.Reservations.AddAsync(new Reservation(model.StartDate, model.EndDate, mainGuest, new List<EntityGuest>() { mainGuest }, null, room));
            else if (mainGuest != null && company != null && room != null)
                await _context.Reservations.AddAsync(new Reservation(model.StartDate, model.EndDate, mainGuest, new List<EntityGuest>() { mainGuest }, company, room));
            else
                throw new Exception("Algo deu errado! Verifique os dados enviados");


        }

        public async Task DoCheckin(int reservationId)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido")
                    .IsGratterThanZeroAndPositive(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido"));

            var reservation = await _context
                                        .Reservations
                                        .Include(x => x.Guests)
                                        .FirstOrDefaultAsync(x => x.Id == reservationId);

            if (reservation != null)
            {
                var folio = reservation.DoCheckIn(reservation);
                _context.Folios.Add(folio);
            }
        }

        public async Task RemoveCompany(int reservationId)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido")
                    .IsGratterThanZeroAndPositive(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido"));

            var reservation = await _context.Reservations.Include(x => x.EntityCompany).FirstOrDefaultAsync(x => x.Id == reservationId);

            if (reservation != null)
            {
                if (reservation.EntityCompanyId != null && reservation.EntityCompany != null)
                    reservation.RemoveCompany();
            }
        }

        public async Task RemoveGuest(int reservationId, int guestId)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido")
                    .IsGratterThanZeroAndPositive(reservationId, nameof(reservationId), $"{nameof(reservationId)} está com um valor inválido")
                    .NotDefault(guestId, nameof(guestId), $"{nameof(guestId)} está com um valor inválido")
                    .IsGratterThanZeroAndPositive(guestId, nameof(guestId), $"{nameof(guestId)} está com um valor inválido"));

            var reservation = await _context.Reservations.Include(x => x.Guests).FirstOrDefaultAsync(x => x.Id == reservationId);
            var guest = await _context.Entities.OfType<EntityGuest>().FirstOrDefaultAsync(x => x.Id == guestId);

            if (reservation != null)
            {
                if (guest != null && reservation.Guests.Any(x => x.Id == guest.Id) && reservation.MainGuestId != guestId)
                    reservation.RemoveGuest(guest);
            }
        }

        public async Task Update(ReservationUpdateDto model)
        {

            Guard.Validate(validator =>
            validator
                .NotNull(model, nameof(model), $"{nameof(model)} é uma referência de um objeto nulo"));

            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == model.ReservationId);


            if (reservation != null)
            {
                if (reservation.StartDate != model.StartDate)
                    reservation.ChangeStartDate(model.StartDate);

                if (reservation.EndDate != model.EndDate)
                    reservation.ChangeEndDate(model.EndDate);
            }


        }
    }
}
