using static PimIVBackend.Models.Entity;

namespace PimIVBackend.Models.CreateDto
{
    public class EntityCompanyCreateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string CEP { get; set; }
        public string Phone { get; set; }
        public string Document { get; set; }
    }
}
