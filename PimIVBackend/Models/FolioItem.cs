using PimIVBackend.Models.Base;
using Validator;

namespace PimIVBackend.Models
{
    public class FolioItem : ModelBase
    {
        public FolioItem()
        {

        }

        public FolioItem(Product product, EntityGuest entityGuest, decimal quantity)
        {
            Guard.Validate(validate =>
                validate
                    .NotNull(product, nameof(product), $"{nameof(product)} é uma referência para um objeto nulo")
                    .NotNull(entityGuest, nameof(entityGuest), $"{nameof(entityGuest)} é uma referência para um objeto nulo")
                    .NotDefault(quantity, nameof(quantity), $"{nameof(quantity)} possui um valor inválido")
                    .NotDefault(product.Price, nameof(product.Price), $"{nameof(product.Price)} possui um valor inválido")
                    .IsGratterThanZeroAndPositive(product.Price, nameof(product.Price), $"{nameof(product.Price)} possui um valor inválido")
                    .IsGratterThanZeroAndPositive(quantity, nameof(quantity), $"{nameof(quantity)} possui um valor inválido")
                    );

            Product = product;
            EntityGuestId = entityGuest.Id;
            Quantity = quantity;
            UnitValue = product.Price;
            TotalValue = product.Price * quantity;
        }

        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public virtual Product Product { get; private set; }
        public int EntityGuestId { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal UnitValue { get; private set; }
        public decimal TotalValue { get; private set; }

    }
}
