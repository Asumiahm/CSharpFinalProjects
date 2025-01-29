using MongoDB.Driver;
using BloodBankAPI.Models;

namespace BloodBankAPI.Services{
    
public class DonorService : IDonorService
{
    private readonly IMongoCollection<Donor> _donors;

    public DonorService(IMongoDatabase database)
    {
        _donors = database.GetCollection<Donor>("BloodDonors");
    }

    public async Task<List<Donor>> GetAllDonorsAsync()
    {
        return await _donors.Find(donor => true).ToListAsync();
    }

    public async Task<Donor> GetDonorByIdAsync(string id)
    {
        return await _donors.Find(donor => donor.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateDonorAsync(Donor donor)
    {
        await _donors.InsertOneAsync(donor);
    }

    public async Task UpdateDonorAsync(string id, Donor donor)
    {
        await _donors.ReplaceOneAsync(d => d.Id == id, donor);
    }

    public async Task DeleteDonorAsync(string id)
    {
        await _donors.DeleteOneAsync(d => d.Id == id);
    }
}
}