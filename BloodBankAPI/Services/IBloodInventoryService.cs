using BloodBankAPI.Models;

namespace BloodBankAPI.Services{

public interface IBloodInventoryService
{
    Task<List<BloodInventory>> GetAllInventoryAsync();
    Task<BloodInventory> GetInventoryByIdAsync(string id);
    Task CreateInventoryAsync(BloodInventory inventory);
    Task UpdateInventoryAsync(string bloodType, int Quantity);
    Task DeleteInventoryAsync(string id);
}
}