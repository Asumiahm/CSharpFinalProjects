using BloodBankAPI.Models;
using BloodBankAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Request>>> GetAllRequests()
        {
            var requests = await _requestService.GetAllRequestsAsync();
            return Ok(requests);
        }

         

        [HttpPost]
        public async Task<ActionResult<Request>> CreateRequest([FromBody] Request request)
        {
            if(request == null){
                return BadRequest("Invalid request data.");
            }

           request.SetId(request.Id);
           var createdRequest = await _requestService.CreateRequestAsync(request);
           return CreatedAtAction(nameof(GetAllRequests), new { id = createdRequest.Id }, createdRequest);
        }
 
    }
}
