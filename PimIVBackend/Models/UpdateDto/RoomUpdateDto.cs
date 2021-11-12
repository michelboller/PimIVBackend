using PimIVBackend.Models.CreateDto;

namespace PimIVBackend.Models.UpdateDto
{
    public class RoomUpdateDto : RoomCreateDto
    {
        public int Id { get; set; }
    }
}
