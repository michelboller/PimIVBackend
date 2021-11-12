using PimIVBackend.Models.CreateDto;
using PimIVBackend.Models.UpdateDto;
using System.Threading.Tasks;

namespace PimIVBackend.Services
{
    public interface IProductServices
    {
        public Task Create(ProductCreateDto model);
        public Task Update(ProductUpdateDto model);
        public Task ActivateInactivate(int id);
    }
}
