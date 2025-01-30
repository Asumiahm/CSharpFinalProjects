using MongoDB.Driver;
using BloodBankAPI.Models;

namespace BloodBankAPI.Services
{
    public class RequestService : IRequestService
    {
        private readonly IMongoCollection<Request> _requests;

        public RequestService(IMongoDatabase database)
        {
           _requests = database.GetCollection<Request>("Requests");
        }

        public async Task<List<Request>> GetAllRequestsAsync()
        {
            try
            {
                return await _requests.Find(request => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve requests. Please try again later.", ex);
            }
            //return await _requests.Find(request => true).ToListAsync();
        }

        public async Task<Request> CreateRequestAsync(Request request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "Request cannot be null.");
                }

                await _requests.InsertOneAsync(request);
                return request;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Argument Error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create request. Please try again later.", ex);
            }
           /* await _requests.InsertOneAsync(request);
            return request;*/
        }

    }
}
