using MongoDB.Driver;
using BloodBankAPI.Models;


namespace BloodBankAPI.Services{

public class DonationService : IDonationService
{
  private readonly IMongoCollection<Donation> _donationCollection;


    public DonationService(IMongoDatabase database)
        {
            _donationCollection = database.GetCollection<Donation>("Donations");
        }

public async Task<Donation> CreateDonationAsync(Donation donation)
{
            try
            {
                if (donation == null)
                {
                    throw new ArgumentNullException(nameof(donation), "Donation data cannot be null");
                }
                await _donationCollection.InsertOneAsync(donation);
                return donation;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Bad Request: {ex.Message}"); // Simulating HTTP 400 Bad Request
            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server Error: {ex.Message}"); // Simulating HTTP 500 Internal Server Error
            }
    /*await _donationCollection.InsertOneAsync(donation);
    return donation;*/
}

public async Task<List<Donation>> GetAllDonationsPaginatedAsync(int page, int pageSize){
            try
            {
            return await _donationCollection
            .Find(d => true)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();            }
            catch (Exception ex)
            {
                throw new Exception($"Internal Server Error: {ex.Message}"); // Simulating HTTP 500 Internal Server Error
            }
   /* return await _donationCollection.Find(_ => true).ToListAsync();*/
}

}
}
