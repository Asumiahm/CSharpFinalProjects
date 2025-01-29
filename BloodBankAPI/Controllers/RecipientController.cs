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
            var recipients = await _recipientService.GetAllRecipientsAsync();
            return Ok(recipients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipientById(string id)
        {
            var recipient = await _recipientService.GetRecipientByIdAsync(id);
            recipient.SetId(recipient.Id);
            if (recipient == null)
                return NotFound("Recipient not found.");

            return Ok(recipient);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipient([FromBody] Recipient recipient)
        {
            if (recipient == null)
                return BadRequest("Invalid recipient data.");
            recipient.SetId(recipient.Id);
            await _recipientService.CreateRecipientAsync(recipient);
            return CreatedAtAction(nameof(GetRecipientById), new { id = recipient.Id }, recipient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipient(string id, [FromBody] Recipient recipient)
        {
            var existingRecipient = await _recipientService.GetRecipientByIdAsync(id);
            if (existingRecipient == null)
                return NotFound("Recipient not found.");

            await _recipientService.UpdateRecipientAsync(id, recipient);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipient(string id)
        {
            var existingRecipient = await _recipientService.GetRecipientByIdAsync(id);
            if (existingRecipient == null)
                return NotFound("Recipient not found.");

            await _recipientService.DeleteRecipientAsync(id);
            return NoContent();
        }
    }
}
