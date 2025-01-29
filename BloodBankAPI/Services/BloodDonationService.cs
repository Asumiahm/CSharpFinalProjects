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
    await _donationCollection.InsertOneAsync(donation);
    return donation;
}

public async Task<List<Donation>> GetAllDonationsAsync()
{
    return await _donationCollection.Find(_ => true).ToListAsync();
}

}
}

