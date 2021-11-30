using PimIVBackend.Models.Base;
using PimIVBackend.Models.Dto;
using System.Collections.Generic;
using System.Linq;
using Validator;

namespace PimIVBackend.Models
{
    public class Folio : ModelBase
    {
        public Folio()
        {

        }

        public Folio(Reservation reservation)
        {
            Guard.Validate(validator => 
                    validator
                        .NotNull(reservation, nameof(reservation), $"{nameof(reservation)} é referencia nula de um objeto")
                );


            FolioItems = new List<FolioItem>();
            Entities = reservation.Guests.Select(x => new FolioEntity(this, x)).ToList();
            Reservation = reservation;
            FolioStatus = FolioStatusEnum.Opened;
        }

        public int Id { get; private set; }
        public List<FolioItem> FolioItems { get; private set; }
        public List<FolioEntity> Entities { get; private set; }
        public int? ReservationId { get;private set; }
        public virtual Reservation Reservation { get; private set; }
        public FolioStatusEnum FolioStatus { get; private set; }
        public enum FolioStatusEnum
        {
            Opened = 1,
            Closed = 2,
        }

        public void PostProduct(FolioItem folioItem)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(folioItem, nameof(folioItem), $"{nameof(folioItem)} é uma referência de objeto nulo"));

            if(FolioStatus == FolioStatusEnum.Opened)
                FolioItems.Add(folioItem);
        }

        public void RemoveProduct(FolioItem folioItem)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(folioItem, nameof(folioItem), $"{nameof(folioItem)} é uma referência de objeto nulo"));

            if(FolioStatus == FolioStatusEnum.Opened)
                FolioItems.Remove(folioItem);
        }

        public ClosedFolioDto CheckOut()
        {
            FolioStatus = FolioStatusEnum.Closed;
            var amount = FolioItems.Select(x => x.TotalValue).DefaultIfEmpty(0).Sum();
            var itemsCount = FolioItems.Select(x => x.Quantity).DefaultIfEmpty(0).Sum();
            return new ClosedFolioDto(itemsCount, amount, Reservation.MainGuest, Id);
        }
    }
}
