using PimIVBackend.Models.CreateDto;

namespace PimIVBackend.Models.UpdateDto
{
    public class EntityCompanyUpdateDto : EntityCompanyCreateDto
    {
        public int Id { get; set; }
    }
}
