using Microsoft.EntityFrameworkCore;
using PimIVBackend.Models;
using PimIVBackend.Models.CreateDto;
using PimIVBackend.Models.UpdateDto;
using PimIVBackend.Services;
using System.Threading.Tasks;
using Validator;

namespace PimIVBackend.Business
{
    public class RoomBusiness : IRoomServices
    {
        private readonly AppDbContext _context;

        public RoomBusiness(AppDbContext context)
        {
            _context = context;
        }

        public async Task ActivateInactivate(int id)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(id, nameof(id), $"{nameof(id)} está com um valor inválido")
                    .IsGratterThanZeroAndPositive(id, nameof(id), $"{nameof(id)} está com um valor inválido"));

            var entity = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
                entity.ActivateInactivate();
        }

        public async Task Create(RoomCreateDto model)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(model, nameof(model), $"{nameof(model)} é uma referência de um objeto nulo"));

            await _context.Rooms.AddAsync(new Room(model.Price, model.Size, model.Code, model.Floor));
        }

        public async Task Update(RoomUpdateDto model)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(model, nameof(model), $"{nameof(model)} é uma referência de um objeto nulo"));

            var entity = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity != null)
            {
                if (!string.IsNullOrWhiteSpace(model.Code))
                    entity.ChangeCode(model.Code);

                if (model.Size > 0)
                    entity.ChangeSize(model.Size);

                if (!string.IsNullOrWhiteSpace(model.Code))
                    entity.ChangeFloor(model.Code);

                if (model.Price > 0)
                    entity.ChangePrice(model.Price);
            }
        }
    }
}
