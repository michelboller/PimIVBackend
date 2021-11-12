using PimIVBackend.Models.Base;
using PimIVBackend.Models.Dto;
using System.Collections.Generic;
using System.Linq;
using Validator;

namespace PimIVBackend.Models
{
    public class Folio : ModelBase
    {
        public Folio(List<Entity> entities, Reservation reservation)
        {
            FolioItems = new List<FolioItem>();
            Entities = entities;
            Reservation = reservation;
            FolioStatus = FolioStatusEnum.Opened;
        }

        public int Id { get; private set; }
        public List<FolioItem> FolioItems { get; private set; }
        public List<Entity> Entities { get; private set; }
        public int? ReservationId { get;private set; }
        public virtual Reservation? Reservation { get; private set; }
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

            FolioItems.Add(folioItem);
        }

        public void RemoveProduct(FolioItem folioItem)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(folioItem, nameof(folioItem), $"{nameof(folioItem)} é uma referência de objeto nulo"));

            FolioItems.Remove(folioItem);
        }

        public ClosedFolioDto CloseFolio()
        {
            FolioStatus = FolioStatusEnum.Closed;
            var amount = FolioItems.Select(x => x.TotalValue).DefaultIfEmpty(0).Sum();
            return new ClosedFolioDto(amount);
        }
    }
}
