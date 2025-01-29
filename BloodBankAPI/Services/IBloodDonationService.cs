using BloodBankAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloodBankAPI.Services;
using MongoDB.Driver;

namespace BloodBankAPI.Services
{
    public interface IDonationService
    {
        Task<Donation> CreateDonationAsync(Donation donation);  
        Task<List<Donation>> GetAllDonationsAsync(); 
    }
}
