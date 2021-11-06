using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimIVBackend.Models
{
    public class EntityCompany : Entity
    {
        public EntityCompany(string name, string address, string cEP, string phone, string document, bool act)
            :base(name, address, cEP, phone, document, EntityDocType.CNPJ, act)
        {
        }
    }
}
