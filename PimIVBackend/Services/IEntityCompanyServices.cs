using PimIVBackend.Models;
using System.Threading.Tasks;

namespace PimIVBackend.Services
{
    public interface IEntityCompanyServices
    {
        Task CreateAsync(EntityCompany model);
        Task UpdateAsync(EntityCompany model);
        Task ActivateInactivate(int id);
    }
}
