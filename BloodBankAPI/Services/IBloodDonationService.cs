using BloodBankAPI.Models;
public interface IBloodDonationService
{
    Task<BloodDonation> RecordDonationAsync(BloodDonation donation);
    Task<IEnumerable<BloodDonation>> GetAllDonationsAsync();
}
