using BloodBankAPI.Models;
using BloodBankAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipientController : ControllerBase
    {
        private readonly IRecipientService _recipientService;

        public RecipientController(IRecipientService recipientService)
        {
            _recipientService = recipientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipients()
        {
            try
            {
                var recipients = await _recipientService.GetAllRecipientsAsync();
                if (recipients == null || !recipients.Any())
                {
                    return NotFound("No recipients found.");
                }
                return Ok(recipients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving recipients: {ex.Message}");
            }
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipientById(string id)
        {
             try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Recipient ID is required.");
                }

                var recipient = await _recipientService.GetRecipientByIdAsync(id);
                if (recipient == null)
                {
                    return NotFound($"Recipient with ID {id} not found.");
                }

                return Ok(recipient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving recipient: {ex.Message}");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipient([FromBody] Recipient recipient)
        {
            try
            {
                if (recipient == null || string.IsNullOrEmpty(recipient.BloodType))
                {
                    return BadRequest("Valid recipient data with a blood type is required.");
                }
                
                recipient.SetId(Guid.NewGuid().ToString()); // Ensure unique ID
                await _recipientService.CreateRecipientAsync(recipient);
                return CreatedAtAction(nameof(GetRecipientById), new { id = recipient.Id }, recipient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating recipient: {ex.Message}");
            }
          
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipient(string id, [FromBody] Recipient recipient)
        {
             try
            {
                if (recipient == null)
                {
                    return BadRequest("Updated recipient data is required.");
                }
                
                var existingRecipient = await _recipientService.GetRecipientByIdAsync(id);
                if (existingRecipient == null)
                {
                    return NotFound($"Recipient with ID {id} not found.");
                }
                
                await _recipientService.UpdateRecipientAsync(id, recipient);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating recipient: {ex.Message}");
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipient(string id)
        {
             try
            {
                var existingRecipient = await _recipientService.GetRecipientByIdAsync(id);
                if (existingRecipient == null)
                {
                    return NotFound($"Recipient with ID {id} not found.");
                }
                
                await _recipientService.DeleteRecipientAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting recipient: {ex.Message}");
            }
           
        }
    }
}
