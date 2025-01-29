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
            return await _requests.Find(request => true).ToListAsync();
        }

        public async Task<Request> CreateRequestAsync(Request request)
        {
            await _requests.InsertOneAsync(request);
            return request;
        }

    }
}
