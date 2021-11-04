using PimIVBackend.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PimIVBackend.Models
{
    public abstract class Entity : ModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CEP { get; set; }
        public string Phone { get; set; }
        public string Document { get; set; }
        public EntityDocType DocType { get; set; }
        public bool Act { get; set; }
        public enum EntityDocType
        {
            RG = 1,
            CPF = 2,
            CNPJ = 3
        }
    }
}
