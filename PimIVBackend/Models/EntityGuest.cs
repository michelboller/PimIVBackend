using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Validator;

namespace PimIVBackend.Models
{
    public class EntityGuest : Entity
    {
        public EntityGuest() { }
        public EntityGuest(string name, string address, string cEP, string phone, string document, EntityDocType docType, bool act, Gender entityGender)
            : base(name, address, cEP, phone, document, docType, act)
        {
            Guard.Validate(validator =>
                validator
                    .IsXGratterThanY(entityGender.GetHashCode(), 2, nameof(entityGender), $"{nameof(entityGender)} possui um valor maior que o permitido (2)")
                    .IsXGratterThanY(0, entityGender.GetHashCode(), nameof(entityGender), $"{nameof(entityGender)} possui um valor menor que o permitido (0)"));

            EntityGender = entityGender;
        }

        public Gender EntityGender { get; private set; }

        public void ChangeGender(Gender gender)
        {
            Guard.Validate(validator =>
                validator
                    .IsXGratterThanY(gender.GetHashCode(), 2, nameof(gender), $"{nameof(gender)} possui um valor maior que o permitido (2)")
                    .IsXGratterThanY(0, gender.GetHashCode(), nameof(gender), $"{nameof(gender)} possui um valor menor que o permitido (0)"));

            EntityGender = gender;
        }

        public enum Gender
        {
            NotDefined = 0,
            M = 1,
            F = 2
        }
    }
}
