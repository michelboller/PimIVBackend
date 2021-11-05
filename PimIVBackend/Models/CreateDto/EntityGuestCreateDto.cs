using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static PimIVBackend.Models.Entity;
using static PimIVBackend.Models.EntityGuest;

namespace PimIVBackend.Models.CreateDto
{
    public class EntityGuestCreateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string CEP { get; set; }
        public string Phone { get; set; }
        
        public string Document { get; set; }
        /// <summary>
        /// 1 - RG
        /// 2 - CPF
        /// 3 - CNPJ
        /// </summary>
        public EntityDocType DocType { get; set; }
        /// <summary>
        /// 0 - Not defined
        /// 1 - Male
        /// 2 - Female
        /// </summary>
        public Gender Gender { get; set; }
    }
}
