using Microsoft.AspNetCore.Mvc;
using BloodBankAPI.Models;
using BloodBankAPI.Services;

namespace BloodBankAPI.Controllers{

[ApiController]
[Route("api/[controller]")]
public class BloodDonorController : ControllerBase
{
    private readonly IBloodDonorService _bloodDonorService;

    public BloodDonorController(IBloodDonorService bloodDonorService)
    {
        _bloodDonorService = bloodDonorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDonations()
    {
        var donations = await _bloodDonorService.GetAllDonorsAsync();
        return Ok(donations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDonationById(string id)
    {
        var donation = await _bloodDonorService.GetDonorByIdAsync(id);
        if (donation == null)
        {
            return NotFound();
        }
        return Ok(donation);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDonation([FromBody] BloodDonor bloodDonation)
    {
        await _bloodDonorService.CreateDonorAsync(bloodDonation);
        return CreatedAtAction(nameof(GetDonationById), new { id = bloodDonation.Id }, bloodDonation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDonation(string id, [FromBody] BloodDonor bloodDonation)
    {
        await _bloodDonorService.UpdateDonorAsync(id, bloodDonation); // Call without assignment
        return NoContent(); // Return a response indicating success
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDonation(string id)
    {
        await _bloodDonorService.DeleteDonorAsync(id); // Call without assignment
        return NoContent(); // Return a response indicating success
    }
  }
}
