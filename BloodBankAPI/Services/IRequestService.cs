using BloodBankAPI.Models;

namespace BloodBankAPI.Services
{
    public interface IRequestService
    {
        Task<List<Request>> GetAllRequestsAsync();
    
        Task<Request>CreateRequestAsync(Request request);

    }
}
