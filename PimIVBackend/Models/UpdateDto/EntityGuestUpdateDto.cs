using PimIVBackend.Models.CreateDto;

namespace PimIVBackend.Models.UpdateDto
{
    public class EntityGuestUpdateDto : EntityGuestCreateDto
    {
        public int Id { get; set; }
    }
}
