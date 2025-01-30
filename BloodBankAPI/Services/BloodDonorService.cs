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
            try
            {
                return await _donors.Find(donor => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server Error: {ex.Message}");
            }
        /*return await _donors.Find(donor => true).ToListAsync();*/
    }

    public async Task<Donor> GetDonorByIdAsync(string id)
    {
        try
            {
                var donor = await _donors.Find(donor => donor.Id == id).FirstOrDefaultAsync();
                if (donor == null)
                {
                    throw new Exception("Donor not found"); 
                }
                return donor;
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server Error: {ex.Message}");
            }
        /*return await _donors.Find(donor => donor.Id == id).FirstOrDefaultAsync();*/
    }

    public async Task CreateDonorAsync(Donor donor)
    {
        try
            {
                if (donor == null)
                {
                    throw new ArgumentNullException(nameof(donor), "Donor data cannot be null");
                }
                await _donors.InsertOneAsync(donor);
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Bad Request: {ex.Message}"); 
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server Error: {ex.Message}"); 
            }
        //await _donors.InsertOneAsync(donor);
    }

    public async Task UpdateDonorAsync(string id, Donor donor)
    {
        try
            {
                var result = await _donors.ReplaceOneAsync(d => d.Id == id, donor);
                if (result.MatchedCount == 0)
                {
                    throw new Exception("Donor not found"); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server Error: {ex.Message}"); 
            }
        //await _donors.ReplaceOneAsync(d => d.Id == id, donor);
    }

    public async Task DeleteDonorAsync(string id)
    {
         try
            {
                var result = await _donors.DeleteOneAsync(d => d.Id == id);
                if (result.DeletedCount == 0)
                {
                    throw new Exception("Donor not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server Error: {ex.Message}"); 
            }
        //await _donors.DeleteOneAsync(d => d.Id == id);
    }
}
}
/*using MongoDB.Driver;
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
        try
            {
                return await _donors.Find(donor => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving donors: {ex.Message}");
            }
           //return await _donors.Find(donor => true).ToListAsync();
    }

    public async Task<Donor> GetDonorByIdAsync(string id)
    {
         try
            {
                var donor = await _donors.Find(d => d.Id == id).FirstOrDefaultAsync();
                if (donor == null)
                {
                    throw new Exception("Donor not found.");
                }
                return donor;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving donor: {ex.Message}");
            }
        //return await _donors.Find(donor => donor.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateDonorAsync(Donor donor)
    {
        try
            {
                await _donors.InsertOneAsync(donor);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating donor: {ex.Message}");
            }
       //await _donors.InsertOneAsync(donor);
    }

    public async Task UpdateDonorAsync(string id, Donor donor)
    {
         try
            {
                var result = await _donors.ReplaceOneAsync(d => d.Id == id, donor);
                if (result.ModifiedCount == 0)
                {
                    throw new Exception("Donor not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating donor: {ex.Message}");
            }
        //await _donors.ReplaceOneAsync(d => d.Id == id, donor);
    }

    public async Task DeleteDonorAsync(string id)
    {
        try
            {
                var result = await _donors.DeleteOneAsync(d => d.Id == id);
                if (result.DeletedCount == 0)
                {
                    throw new Exception("Donor not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting donor: {ex.Message}");
            }
        //await _donors.DeleteOneAsync(d => d.Id == id);
    }
}
}*/