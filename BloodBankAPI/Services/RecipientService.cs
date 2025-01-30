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
            try
            {
                return await _recipients.Find(recipient => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve recipients. Please try again later.", ex);
            }
            //return await _recipients.Find(recipient => true).ToListAsync();
        }

        public async Task<Recipient> GetRecipientByIdAsync(string id)
        {
             try
            {
                var recipient = await _recipients.Find(r => r.Id == id).FirstOrDefaultAsync();
                if (recipient == null)
                {
                    throw new KeyNotFoundException("Recipient not found.");
                }
                return recipient;
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve recipient by ID. Please try again later.", ex);
            }
            //return await _recipients.Find(recipient => recipient.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateRecipientAsync(Recipient recipient)
        {
             try
            {
                if (recipient == null)
                {
                    throw new ArgumentNullException(nameof(recipient), "Recipient cannot be null.");
                }

                await _recipients.InsertOneAsync(recipient);
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Argument Error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create recipient. Please try again later.", ex);
            }
            //await _recipients.InsertOneAsync(recipient);
        }

        public async Task UpdateRecipientAsync(string id, Recipient recipient)
        {
             try
            {
                var existingRecipient = await _recipients.Find(r => r.Id == id).FirstOrDefaultAsync();
                if (existingRecipient == null)
                {
                    throw new KeyNotFoundException("Recipient not found.");
                }

                await _recipients.ReplaceOneAsync(r => r.Id == id, recipient);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update recipient. Please try again later.", ex);
            }
            //await _recipients.ReplaceOneAsync(r => r.Id == id, recipient);
        }

        public async Task DeleteRecipientAsync(string id)
        {
            try
            {
                var deleteResult = await _recipients.DeleteOneAsync(r => r.Id == id);
                if (deleteResult.DeletedCount == 0)
                {
                    throw new KeyNotFoundException("Recipient not found.");
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception($"Error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete recipient. Please try again later.", ex);
            }
        
            //await _recipients.DeleteOneAsync(r => r.Id == id);
        }
    }
}
