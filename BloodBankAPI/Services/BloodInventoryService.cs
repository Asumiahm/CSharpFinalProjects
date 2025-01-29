using MongoDB.Driver;
using BloodBankAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloodBankAPI.Services
{
    public class BloodInventoryService : IBloodInventoryService
    {
        private readonly IMongoCollection<Inventory> _inventory;

        public BloodInventoryService(IMongoDatabase database)
        {
            _inventory = database.GetCollection<Inventory>("BloodInventory");
        }

        // Get all inventory items
        public async Task<IEnumerable<Inventory>> GetAllInventoryAsync()
        {
            return await _inventory.Find(inventory => true).ToListAsync();
        }

        public async Task<Inventory> GetInventoryByIdAsync(string id)
        {
            return await _inventory.Find(inventory => inventory.Id == id).FirstOrDefaultAsync();
        }
        public async Task UpdateInventoryAsync(string id, Inventory updatedInventory)
        {
            var existingInventory = await _inventory.Find(i => i.Id == id).FirstOrDefaultAsync();
            await _inventory.ReplaceOneAsync(i => i.Id == id, updatedInventory);
        }

        public async Task CreateInventoryAsync(Inventory inventory)
        {
            await _inventory.InsertOneAsync(inventory);
        }
        public async Task DeleteInventoryAsync(string id)
        {
            await _inventory.DeleteOneAsync(i => i.Id == id);
        }
    }
}
