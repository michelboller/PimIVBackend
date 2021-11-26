using PimIVBackend.Models.CreateDto;
using PimIVBackend.Models.UpdateDto;
using System.Threading.Tasks;

namespace PimIVBackend.Services
{
    public interface IReservationServices
    {
        public Task Create(ReservationCreateDto model);
        public Task Update(ReservationUpdateDto model);
        public Task AddGuest(int reservationId, int guestId);
        public Task RemoveGuest(int reservationId, int guestId);
        public Task ChangeRoom(int reservationId, int roomId);
        public Task AddCompany(int reservationId, int companyId);
        public Task ChangeCompany(int reservationId, int companyId);
        public Task RemoveCompany(int reservationId);
    }
}
