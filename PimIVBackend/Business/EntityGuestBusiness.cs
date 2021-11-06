using Microsoft.EntityFrameworkCore;
using PimIVBackend.Models;
using PimIVBackend.Models.UpdateDto;
using PimIVBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Validator;

namespace PimIVBackend.Business
{
    public class EntityGuestBusiness : IEntityGuestServices
    {
        private readonly AppDbContext _context;

        public EntityGuestBusiness(AppDbContext context)
        {
            _context = context;
        }

        public async Task ActivateInactivate(int id)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(id, nameof(id), $"{nameof(id)} não possui um valor definido")
                    .IsGratterThanZero(id, nameof(id), $"{nameof(id)} possui um valor menor que zero"));

            var entity = await _context.Entities.FirstOrDefaultAsync(x => x.Id == id);
            
            if(entity != null)
                entity.ChangeAct(!entity.Act);
        }

        public async Task CreateAsync(EntityGuest model)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(model, nameof(model), $"{nameof(model)} está nulo e por isso não foi possível criar uma empresa"));

            await _context.Entities.AddAsync(model);
        }

        public async Task UpdateAsync(EntityGuestUpdateDto model)
        {
            Guard.Validate(validator =>
                validator
                    .NotDefault(model.Id, nameof(model.Id), $"{nameof(model.Id)} não possui um valor definido")
                    .IsGratterThanZero(model.Id, nameof(model.Id), $"{nameof(model.Id)} possui um valor menor que zero"));

            var entity = await _context.Entities.OfType<EntityGuest>().FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity != null)
            {
                entity.ChangeAddress(model.Address);
                entity.ChangeCEP(model.CEP);
                entity.ChangeName(model.Name);
                entity.ChangePhone(model.Phone);
                entity.ChangeDocument(model.Document);
                entity.ChangeDocType(model.DocType);
                entity.ChangeGender(model.Gender);
            }
            else
            {
                throw new Exception("Referência nula para instância de objeto");
            }
        }
    }
}
