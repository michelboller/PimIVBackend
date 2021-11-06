using PimIVBackend.Models;
using PimIVBackend.Models.UpdateDto;
using System.Threading.Tasks;

namespace PimIVBackend.Services
{
    public interface IEntityCompanyServices
    {
        Task CreateAsync(EntityCompany model);
        Task UpdateAsync(EntityCompanyUpdateDto model);
        Task ActivateInactivate(int id);
    }
}
