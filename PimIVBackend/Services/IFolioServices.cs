using PimIVBackend.Models;
using PimIVBackend.Models.Dto;
using System.Threading.Tasks;

namespace PimIVBackend.Services
{
    public interface IFolioServices
    {
        Task PostProduct(int folioId, int productId, int guestId, decimal quantity);
        Task RemoveProduct(int folioId, int folioItemId);
        Task<ClosedFolioDto> DoCheckOut(int folioId);
    }
}
