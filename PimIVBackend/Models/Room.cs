using PimIVBackend.Models.Base;
using Validator;

namespace PimIVBackend.Models
{
    public class Room : ModelBase
    {
        public Room(decimal price, int size, string code = null, string floor = null)
        {
            Guard.Validate(validate =>
                validate
                    .IsGratterThanZeroAndPositive(price, nameof(price), $"{nameof(price)} possui um valor menor ou igual a zero")
                    .IsGratterThanZeroAndPositive(size, nameof(size), $"{nameof(size)} possui um valor menor ou igual a zero"));

            Code = code;
            Floor = floor;
            Price = price;
            Size = size;
            Act = true;
        }

        public int Id { get; private set; }
#nullable enable
        public string? Code { get; private set; }
        public string? Floor { get; private set; }
#nullable disable
        public decimal Price { get; private set; }
        public int Size { get; private set; } //The amount of people
        public bool Act { get; set; }

        public void ChangePrice(decimal price)
        {
            Guard.Validate(validator =>
                validator
                    .IsGratterThanZeroAndPositive(price, nameof(price), $"{nameof(price)} possui um valor menor ou igual a zero"));

            Price = price;
        }

        public void ChangeSize(int size)
        {
            Guard.Validate(validator =>
                validator
                    .IsGratterThanZeroAndPositive(size, nameof(size), $"{nameof(size)} possui um valor menor ou igual a zero"));

            Size = size;
        }

        /// <summary>
        /// Call this method with no params to set the Room Code to null
        /// </summary>
        /// <param name="code"></param>
        public void ChangeCode(string code = null)
        {
            Code = code;
        }

        /// <summary>
        /// Call this method with no params to set the Room Floor to null 
        /// </summary>
        /// <param name="floor"></param>
        public void ChangeFloor(string floor = null)
        {
            Floor = floor;
        }

        public void ActivateInactivate()
        {
            Act = !Act;
        }
    }
}
