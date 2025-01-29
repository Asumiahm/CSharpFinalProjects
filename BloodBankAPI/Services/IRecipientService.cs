using BloodBankAPI.Models;

namespace BloodBankAPI.Services
{
    public interface IRecipientService
    {
        Task<List<Recipient>> GetAllRecipientsAsync();
        Task<Recipient> GetRecipientByIdAsync(string id);
        Task CreateRecipientAsync(Recipient recipient);
        Task UpdateRecipientAsync(string id, Recipient recipient);
        Task DeleteRecipientAsync(string id);
    }
}
