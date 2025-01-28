using MongoDB.Driver;
using BloodBankAPI.Models;


namespace BloodBankAPI.Services{

public class BloodDonationService : IBloodDonationService
{
    private readonly IMongoCollection<BloodDonation> _donationCollection;
    private readonly IBloodInventoryService _inventoryService;

    public BloodDonationService(IMongoDatabase database, IBloodInventoryService inventoryService)
    {
        _donationCollection = database.GetCollection<BloodDonation>("BloodDonations");
        _inventoryService = inventoryService;
    }

    public async Task<BloodDonation> RecordDonationAsync(BloodDonation donation)
    {
        await _donationCollection.InsertOneAsync(donation);
        await _inventoryService.UpdateInventoryAsync(donation.BloodType, donation.QuantityInUnits);
        return donation;
    }

    // Get all blood donations
    public async Task<IEnumerable<BloodDonation>> GetAllDonationsAsync()
    {
        return await _donationCollection.Find(FilterDefinition<BloodDonation>.Empty).ToListAsync();
    }
}
}
