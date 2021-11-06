using PimIVBackend.Models;
using PimIVBackend.Models.UpdateDto;
using System.Threading.Tasks;

namespace PimIVBackend.Services
{
    public interface IEntityGuestServices
    {
        Task CreateAsync(EntityGuest model);
        Task UpdateAsync(EntityGuestUpdateDto model);
        Task ActivateInactivate(int id);
    }
}
