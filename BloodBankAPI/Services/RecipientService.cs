using MongoDB.Driver;
using BloodBankAPI.Models;

namespace BloodBankAPI.Services
{
    public class RecipientService : IRecipientService
    {
        private readonly IMongoCollection<Recipient> _recipients;

        public RecipientService(IMongoDatabase database)
        {
            _recipients = database.GetCollection<Recipient>("Recipients");
        }

        public async Task<List<Recipient>> GetAllRecipientsAsync()
        {
            return await _recipients.Find(recipient => true).ToListAsync();
        }

        public async Task<Recipient> GetRecipientByIdAsync(string id)
        {
            return await _recipients.Find(recipient => recipient.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateRecipientAsync(Recipient recipient)
        {
            await _recipients.InsertOneAsync(recipient);
        }

        public async Task UpdateRecipientAsync(string id, Recipient recipient)
        {
            await _recipients.ReplaceOneAsync(r => r.Id == id, recipient);
        }

        public async Task DeleteRecipientAsync(string id)
        {
            await _recipients.DeleteOneAsync(r => r.Id == id);
        }
    }
}
