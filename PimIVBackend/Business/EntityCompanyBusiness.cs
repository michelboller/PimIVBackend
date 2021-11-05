using Microsoft.EntityFrameworkCore;
using PimIVBackend.Models;
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
            entity.Act = !entity.Act;
        }

        public async Task CreateAsync(EntityCompany model)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(model, nameof(model), $"{nameof(model)} está com valor nulo")
                    .IsXGratterThanY(model.DocType.GetHashCode(), 3, nameof(model.DocType), $"{nameof(model.DocType)} possui um valor maior que o permitido (3)")
                    .IsXGratterThanY(3, model.DocType.GetHashCode(), nameof(model.DocType), $"{nameof(model.DocType)} possui um valor menor que o permitido (3)")
                    .NotNullOrEmptyString(model.Name, nameof(model.Name), $"{nameof(model.Name)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.Address, nameof(model.Address), $"{nameof(model.Address)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.CEP, nameof(model.CEP), $"{nameof(model.CEP)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.Phone, nameof(model.Phone), $"{nameof(model.Phone)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.Document, nameof(model.Document), $"{nameof(model.Document)} não possui valor ou é uma string em branco")
                    );

            model.Act = true;
            await _context.Entities.AddAsync(model);
        }

        public async Task UpdateAsync(EntityCompany model)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(model, nameof(model), $"{nameof(model)} está com valor nulo")
                    .IsXGratterThanY(model.DocType.GetHashCode(), 3, nameof(model.DocType), $"{nameof(model.DocType)} possui um valor maior que o permitido (3)")
                    .IsXGratterThanY(3, model.DocType.GetHashCode(), nameof(model.DocType), $"{nameof(model.DocType)} possui um valor menor que o permitido (3)")
                    .NotNullOrEmptyString(model.Name, nameof(model.Name), $"{nameof(model.Name)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.Address, nameof(model.Address), $"{nameof(model.Address)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.CEP, nameof(model.CEP), $"{nameof(model.CEP)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.Phone, nameof(model.Phone), $"{nameof(model.Phone)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.Document, nameof(model.Document), $"{nameof(model.Document)} não possui valor ou é uma string em branco")
                    );

            var entity = await _context.Entities.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity != null)
            {
                entity.Address = model.Address;
                entity.CEP = model.CEP;
                entity.Name = model.Name;
                entity.Phone = model.Phone;
                entity.Document = model.Document;
                entity.Act = true;
            }
            else
            {
                throw new Exception("Referência nula para instância de objeto");
            }

        }
    }
}
