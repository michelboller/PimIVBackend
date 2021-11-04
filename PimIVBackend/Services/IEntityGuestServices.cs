using PimIVBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimIVBackend.Services
{
    public interface IEntityGuestServices
    {
        Task CreateAsync(EntityGuest model);
        Task UpdateAsync(EntityGuest model);
        Task ActivateInactivate(int id);
    }
}
