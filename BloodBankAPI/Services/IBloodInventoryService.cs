using BloodBankAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloodBankAPI.Services
{
    public interface IBloodInventoryService
    {
        Task<IEnumerable<Inventory>> GetAllInventoryAsync();
        Task<Inventory> GetInventoryByIdAsync(string id);
        Task CreateInventoryAsync(Inventory inventory);
        Task UpdateInventoryAsync(string id, Inventory updatedInventory);
        Task DeleteInventoryAsync(string id);
    }
}
