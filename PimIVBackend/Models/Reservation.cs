using PimIVBackend.Models.Base;
using System;
using System.Collections.Generic;
using Validator;

namespace PimIVBackend.Models
{
    public class Reservation : ModelBase
    {
        public Reservation(DateTime startDate, DateTime endDate, EntityGuest mainGuest, List<EntityGuest> guests, EntityCompany entityCompany, Room room)
        {
            Guard.Validate(validate =>
                validate
                    .NotDefault(startDate, nameof(startDate), $"{nameof(startDate)} possui um valor inválido")
                    .NotDefault(endDate, nameof(endDate), $"{nameof(endDate)} possui um valor inválido")
                    .IsGratterThanOrEqualsToToday(startDate, nameof(startDate), $"{nameof(startDate)} possui uma data menor a do dia de hoje")
                    .IsGratterThanOrEqualsToToday(endDate, nameof(endDate), $"{nameof(endDate)} possui uma data menor a do dia de hoje")
                    .NotNull(mainGuest, nameof(mainGuest), $"{nameof(mainGuest)} é uma referência para um objeto nulo")
                    .IsNotNullAndNotEmpty(guests, nameof(guests), $"A lista de {nameof(guests)} está vazia")
                    .NotNull(room, nameof(room), $"{nameof(room)} é uma referência para um objeto nulo")
                    );

            StartDate = startDate;
            EndDate = endDate;
            MainGuest = mainGuest;
            Guests = guests;
            EntityCompany = entityCompany;
            Room = room;
        }

        public int Id { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int MainGuestId { get; private set; }
        public virtual EntityGuest MainGuest { get; private set; }
        public List<EntityGuest> Guests { get; private set; }
        public int? EntityCompanyId { get; private set; }
        public virtual EntityCompany? EntityCompany { get; private set; }
        public int RoomId { get; private set; }
        public virtual Room Room { get; private set; }

        public void ChangeStartDate(DateTime date)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(date, nameof(date), $"{nameof(date)} possui um valor inválido")
                    .IsGratterThanOrEqualsToToday(date, nameof(date), $"{nameof(date)} possui uma data menor a do dia de hoje")
                    .IsXDateGratterThanYDate(EndDate, date, nameof(date), $"{nameof(date)} possui uma data inválida pois seu valor ultrapassa a data de fim da reserva")
                    );

            StartDate = date;
        }

        public void ChangeEndDate(DateTime date)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(date, nameof(date), $"{nameof(date)} possui um valor inválido")
                    .IsGratterThanOrEqualsToToday(date, nameof(date), $"{nameof(date)} possui uma data menor a do dia de hoje")
                    .IsXDateGratterThanYDate(date, StartDate, nameof(date), $"{nameof(date)} possui uma data inválida pois seu valor indica uma data de antes do início da reserva")
                    );

            EndDate = date;
        }

        public void ChangeMainGuest(int mainGuestId, EntityGuest mainGuest)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(mainGuestId, nameof(mainGuestId), $"{nameof(mainGuestId)} possui um valor inválido")
                    .IsGratterThanZeroAndPositive(mainGuestId, nameof(mainGuestId), $"{nameof(mainGuestId)} possui um valor menor ou igual a zero")
                    .NotNull(mainGuest, nameof(mainGuest), $"{nameof(mainGuest)} é uma referência para um objeto nulo")
                    );

            MainGuestId = mainGuestId;
            MainGuest = mainGuest;
        }

        public void AddGuests(EntityGuest guest)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(guest, nameof(guest), $"{nameof(guest)} é uma referência para um objeto nulo")
                    );

            Guests.Add(guest);
        }

        public void ChangeRoom(int roomId, Room room)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(roomId, nameof(roomId), $"{nameof(roomId)} possui um valor inválido")
                    .IsGratterThanZeroAndPositive(roomId, nameof(roomId), $"{nameof(roomId)} possui um valor menor ou igual a zero")
                    .NotNull(room, nameof(room), $"{nameof(room)} é uma referência para um objeto nulo")
                    );

            //verificar se o quarto está disponivel (business)

            RoomId = roomId;
            Room = room;
        }
    }
}
