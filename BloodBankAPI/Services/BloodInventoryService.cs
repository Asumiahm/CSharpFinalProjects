using MongoDB.Driver;
using BloodBankAPI.Models;

namespace BloodBankAPI.Services{

public class BloodInventoryService : IBloodInventoryService
{
    private readonly IMongoCollection<BloodInventory> _inventory;

    public BloodInventoryService(IMongoDatabase database)
    {
        _inventory = database.GetCollection<BloodInventory>("BloodInventory");
    }

    public async Task<List<BloodInventory>> GetAllInventoryAsync()
    {
        return await _inventory.Find(inventory => true).ToListAsync();
    }

    public async Task<BloodInventory> GetInventoryByIdAsync(string id)
    {
        return await _inventory.Find(inventory => inventory.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateInventoryAsync(BloodInventory inventory)
    {
        await _inventory.InsertOneAsync(inventory);
    }

    public async Task UpdateInventoryAsync(string bloodType, int Quantity){
    await _inventory.ReplaceOneAsync(i => i.BloodType == bloodType, 
        new BloodInventory { BloodType = bloodType, Quantity = Quantity });}


    public async Task DeleteInventoryAsync(string id)
    {
        await _inventory.DeleteOneAsync(i => i.Id == id);
    }
}
}