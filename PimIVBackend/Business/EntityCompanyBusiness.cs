using Microsoft.EntityFrameworkCore;
using PimIVBackend.Models;
using PimIVBackend.Models.UpdateDto;
using PimIVBackend.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Validator;

namespace PimIVBackend.Business
{
    public class EntityCompanyBusiness : IEntityCompanyServices
    {
        private readonly AppDbContext _context;

        public EntityCompanyBusiness(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task ActivateInactivate(int id)
        {
            Guard.Validate(validator =>
                validator
                    .IsGratterThanZero(id, nameof(id), $"{nameof(id)} está com um valor inválido")
                    .NotDefault(id, nameof(id), $"{nameof(id)} está com um valor inválido"));

            var entity = await _context.Entities.OfType<EntityCompany>().FirstOrDefaultAsync(x => x.Id == id);

            if(entity != null)
                entity.ChangeAct(!entity.Act);
        }

        public async Task CreateAsync(EntityCompany model)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(model, nameof(model), $"{nameof(model)} está nulo e por isso não foi possível criar uma empresa"));

            await _context.Entities.AddAsync(model);
        }

        public async Task UpdateAsync(EntityCompanyUpdateDto model)
        {
            Guard.Validate(validator =>
                validator
                    .IsGratterThanZero(model.Id, nameof(model.Id), $"{nameof(model.Id)} está com um valor inválido")
                    .NotDefault(model.Id, nameof(model.Id), $"{nameof(model.Id)} está com um valor inválido"));

            var entity = await _context.Entities.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity != null)
            {
                entity.ChangeAddress(model.Address);
                entity.ChangeCEP(model.CEP);
                entity.ChangeName(model.Name);
                entity.ChangePhone(model.Phone);
                entity.ChangeDocument(model.Document);
            }
            else
            {
                throw new Exception("Referência nula para instância de objeto");
            }
        }
    }
}
