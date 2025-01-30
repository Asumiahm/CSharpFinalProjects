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
             try
            {
                var requests = await _requestService.GetAllRequestsAsync();
                if (requests == null || requests.Count == 0)
                {
                    return NotFound("No requests found.");
                }
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
           
        }

         

        [HttpPost]
        public async Task<ActionResult<Request>> CreateRequest([FromBody] Request request)
        {
             try
            {
                if (request == null)
                {
                    return BadRequest("Invalid request data.");
                }

                if (string.IsNullOrEmpty(request.Id))
                {
                    return BadRequest("Request ID is required.");
                }

                request.SetId(request.Id);
                var createdRequest = await _requestService.CreateRequestAsync(request);

                if (createdRequest == null)
                {
                    return StatusCode(500, "Error creating the request.");
                }

                return CreatedAtAction(nameof(GetAllRequests), new { id = createdRequest.Id }, createdRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
           
        }
 
    }

