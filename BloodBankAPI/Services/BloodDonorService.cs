using MongoDB.Driver;
using BloodBankAPI.Models;

namespace BloodBankAPI.Services{
    
public class BloodDonorService : IBloodDonorService
{
    private readonly IMongoCollection<BloodDonor> _donors;

    public BloodDonorService(IMongoDatabase database)
    {
        _donors = database.GetCollection<BloodDonor>("BloodDonors");
    }

    public async Task<List<BloodDonor>> GetAllDonorsAsync()
    {
        return await _donors.Find(donor => true).ToListAsync();
    }

    public async Task<BloodDonor> GetDonorByIdAsync(string id)
    {
        return await _donors.Find(donor => donor.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateDonorAsync(BloodDonor donor)
    {
        await _donors.InsertOneAsync(donor);
    }

    public async Task UpdateDonorAsync(string id, BloodDonor donor)
    {
        await _donors.ReplaceOneAsync(d => d.Id == id, donor);
    }

    public async Task DeleteDonorAsync(string id)
    {
        await _donors.DeleteOneAsync(d => d.Id == id);
    }
}
}