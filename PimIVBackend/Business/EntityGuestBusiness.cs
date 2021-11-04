using Microsoft.EntityFrameworkCore;
using PimIVBackend.Models;
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
            entity.Act = !entity.Act;
        }

        public async Task CreateAsync(EntityGuest model)
        {
            Guard.Validate(validator => 
                validator
                    .NotNull(model, nameof(model), $"{nameof(model)} está com valor nulo")
                    .IsGratterThanZero(model.EntityGender.GetHashCode(), nameof(model.EntityGender), $"{nameof(model.EntityGender)} possiu valor menor que o permitido (zero)")
                    .IsXGratterThanY(model.EntityGender.GetHashCode(), 2, nameof(model.EntityGender), $"{nameof(model.EntityGender)} possui um valor maior que o permitido (2)") //Caso aumentar o enum mudar aqui
                    .IsXGratterThanY(model.DocType.GetHashCode(), 3, nameof(model.DocType), $"{nameof(model.DocType)} possui um valor maior que o permitido (3)")
                    .IsXGratterThanY(1, model.DocType.GetHashCode(), nameof(model.DocType), $"{nameof(model.DocType)} possui um valor menor que o permitido (1)")
                    .NotNullOrEmptyString(model.Name, nameof(model.Name), $"{nameof(model.Name)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.Address, nameof(model.Address), $"{nameof(model.Address)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.CEP, nameof(model.CEP), $"{nameof(model.CEP)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.Phone, nameof(model.Phone), $"{nameof(model.Phone)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.Document, nameof(model.Document), $"{nameof(model.Document)} não possui valor ou é uma string em branco")
                    );

            model.Act = true;
            await _context.Entities.AddAsync(model);
        }

        public async Task UpdateAsync(EntityGuest model)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(model.Id, nameof(model.Id), $"{nameof(model.Id)} está com um valor nulo")
                    .IsGratterThanZero(model.Id, nameof(model.Id), $"{nameof(model.Id)} está com um valor nulo")
                    .NotNull(model, nameof(model), $"{nameof(model)} está com valor nulo")
                    .IsGratterThanZero(model.EntityGender.GetHashCode(), nameof(model.EntityGender), $"{nameof(model.EntityGender)} possiu valor menor que o permitido (zero)")
                    .IsXGratterThanY(model.EntityGender.GetHashCode(), 2, nameof(model.EntityGender), $"{nameof(model.EntityGender)} possui um valor maior que o permitido (2)") //Caso aumentar o enum mudar aqui
                    .IsXGratterThanY(model.DocType.GetHashCode(), 3, nameof(model.DocType), $"{nameof(model.DocType)} possui um valor maior que o permitido (3)")
                    .IsXGratterThanY(1, model.DocType.GetHashCode(), nameof(model.DocType), $"{nameof(model.DocType)} possui um valor menor que o permitido (1)")
                    .NotNullOrEmptyString(model.Name, nameof(model.Name), $"{nameof(model.Name)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.Address, nameof(model.Address), $"{nameof(model.Address)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.CEP, nameof(model.CEP), $"{nameof(model.CEP)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.Phone, nameof(model.Phone), $"{nameof(model.Phone)} não possui valor ou é uma string em branco")
                    .NotNullOrEmptyString(model.Document, nameof(model.Document), $"{nameof(model.Document)} não possui valor ou é uma string em branco")
                    );

            var entity = await _context.Entities.OfType<EntityGuest>().FirstOrDefaultAsync(x => x.Id == model.Id);

            entity.Address = model.Address;
            entity.CEP = model.CEP;
            entity.Name = model.Name;
            entity.Phone = model.Phone;
            entity.Document = model.Document;
            entity.DocType = model.DocType;
            entity.EntityGender = model.EntityGender;
        }
    }
}
