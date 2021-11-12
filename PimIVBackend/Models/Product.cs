using PimIVBackend.Models.Base;
using PimIVBackend.Models.UpdateDto;
using Validator;

namespace PimIVBackend.Models
{
    public class Product : ModelBase
    {
        public Product(string name, decimal price)
        {
            Guard.Validate(validate =>
                validate
                    .NotNullOrEmptyString(name, nameof(name), $"{nameof(name)} está em branco, contem espaços em branco ou está nulo")
                    .IsGratterThanZeroAndPositive(price, nameof(price), $"{nameof(price)} contem um valor inválido")
                    );

            Name = name;
            Price = price;
            Act = true;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public bool Act { get; private set; }

        public void ChangeProduct(ProductUpdateDto product)
        {
            Guard.Validate(validate =>
                validate
                    .NotNull(product, nameof(product), $"{nameof(product)} é uma referência nula")
                    .NotNullOrEmptyString(product.Name, nameof(product.Name), $"{nameof(product.Name)} está em branco, contem espaços em branco ou está nulo")
                    .IsGratterThanZeroAndPositive(product.Price, nameof(product.Price), $"{nameof(product.Price)} contem um valor inválido")
                    );

            Name = product.Name;
            Price = product.Price;
        }

        public void ActivateInactivate()
        {
            Act = !Act;
        }
    }
}
