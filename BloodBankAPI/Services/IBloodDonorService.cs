using BloodBankAPI.Models;

namespace BloodBankAPI.Services{

public interface IDonorService
{
    Task<List<Donor>> GetAllDonorsAsync();
    Task<Donor> GetDonorByIdAsync(string id);
    Task CreateDonorAsync(Donor donor);
    Task UpdateDonorAsync(string id, Donor donor);
    Task DeleteDonorAsync(string id);
}
}