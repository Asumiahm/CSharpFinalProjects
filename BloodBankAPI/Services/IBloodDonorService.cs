using BloodBankAPI.Models;

namespace BloodBankAPI.Services{

public interface IBloodDonorService
{
    Task<List<BloodDonor>> GetAllDonorsAsync();
    Task<BloodDonor> GetDonorByIdAsync(string id);
    Task CreateDonorAsync(BloodDonor donor);
    Task UpdateDonorAsync(string id, BloodDonor donor);
    Task DeleteDonorAsync(string id);
}
}