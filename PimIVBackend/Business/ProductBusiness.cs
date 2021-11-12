using Microsoft.EntityFrameworkCore;
using PimIVBackend.Models;
using PimIVBackend.Models.CreateDto;
using PimIVBackend.Models.UpdateDto;
using PimIVBackend.Services;
using System.Threading.Tasks;
using Validator;

namespace PimIVBackend.Business
{
    public class ProductBusiness : IProductServices
    {
        private readonly AppDbContext _context;

        public ProductBusiness(AppDbContext context)
        {
            _context = context;
        }

        public async Task ActivateInactivate(int id)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(id, nameof(id), $"{nameof(id)} não possui um valor definido")
                    .IsGratterThanZero(id, nameof(id), $"{nameof(id)} possui um valor menor que zero"));

            var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
                entity.ActivateInactivate();
        }

        public async Task Create(ProductCreateDto model)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(model, nameof(model), $"{nameof(model)} está nulo e por isso não foi possível criar um produto"));


            await _context.Products.AddAsync(new Product(model.Name, model.Price));
        }

        public async Task Update(ProductUpdateDto model)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(model, nameof(model), $"{nameof(model)} está nulo e por isso é possível atualizar o produto")
                    .NotDefault(model.Id, nameof(model.Id), $"{nameof(model.Id)} não possui um valor definido")
                    .IsGratterThanZeroAndPositive(model.Id, nameof(model.Id), $"{nameof(model.Id)} não possui um valor válido")
                );

            var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity != null)
                entity.ChangeProduct(model);
        }
    }
}
