using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimIVBackend.Models
{
    public class EntityCompany : Entity
    {
        public EntityCompany()
        {
            DocType = EntityDocType.CNPJ;
        }

        new public EntityDocType DocType { get; }
    }
}
