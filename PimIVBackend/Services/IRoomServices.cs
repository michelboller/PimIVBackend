using PimIVBackend.Models.CreateDto;
using PimIVBackend.Models.UpdateDto;
using System.Threading.Tasks;

namespace PimIVBackend.Services
{
    public interface IRoomServices
    {
        public Task Create(RoomCreateDto model);
        public Task Update(RoomUpdateDto model);
        public Task ActivateInactivate(int id);
    }
}
