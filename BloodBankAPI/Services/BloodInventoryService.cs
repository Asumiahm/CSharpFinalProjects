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
            _inventory = database.GetCollection<Inventory>("Inventory");
        }

        // Get all inventory items
        public async Task<IEnumerable<Inventory>> GetAllInventoryAsync()
        {
            try
            {
                return await _inventory.Find(inventory => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching inventory: {ex.Message}");
                return new List<Inventory>(); // Return empty list if an error occurs
            }
        }
        /*public async Task<IEnumerable<Inventory>> GetAllInventoryAsync()
        {
            return await _inventory.Find(inventory => true).ToListAsync();
        }*/

        public async Task<Inventory> GetInventoryByIdAsync(string id)
        {
             try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentException("Invalid ID provided.");
                }

                var inventory = await _inventory.Find(inventory => inventory.Id == id).FirstOrDefaultAsync();
                return inventory ?? throw new KeyNotFoundException("Inventory not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching inventory by ID: {ex.Message}");
                return null; // Handle null responses in the controller
            }
            //return await _inventory.Find(inventory => inventory.Id == id).FirstOrDefaultAsync();
        }
        public async Task UpdateInventoryAsync(string id, Inventory updatedInventory)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || updatedInventory == null)
                {
                    throw new ArgumentException("Invalid ID or inventory data provided.");
                }

                var existingInventory = await _inventory.Find(i => i.Id == id).FirstOrDefaultAsync();
                if (existingInventory == null)
                {
                    throw new KeyNotFoundException("Inventory not found.");
                }

                await _inventory.ReplaceOneAsync(i => i.Id == id, updatedInventory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating inventory: {ex.Message}");
            }
            /*var existingInventory = await _inventory.Find(i => i.Id == id).FirstOrDefaultAsync();
            await _inventory.ReplaceOneAsync(i => i.Id == id, updatedInventory);*/
        }

        public async Task<Inventory> CreateInventoryAsync(Inventory inventory)
        {
            try
            {
                if (inventory == null || string.IsNullOrWhiteSpace(inventory.BloodType) || inventory.Quantity < 1)
                {
                    throw new ArgumentException("Invalid inventory data.");
                }

                await _inventory.InsertOneAsync(inventory);
                return inventory;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating inventory: {ex.Message}");
                return null;
            }
            /*await _inventory.InsertOneAsync(inventory);
            return inventory;*/
        }
        public async Task DeleteInventoryAsync(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    throw new ArgumentException("Invalid ID provided.");
                }

                var existingInventory = await _inventory.Find(i => i.Id == id).FirstOrDefaultAsync();
                if (existingInventory == null)
                {
                    throw new KeyNotFoundException("Inventory not found.");
                }

                await _inventory.DeleteOneAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting inventory: {ex.Message}");
            }
            //await _inventory.DeleteOneAsync(i => i.Id == id);
        }
    }
}


