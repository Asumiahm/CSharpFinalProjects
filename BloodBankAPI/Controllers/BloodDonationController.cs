using Microsoft.AspNetCore.Mvc;
using BloodBankAPI.Services;
using BloodBankAPI.Models;


namespace BloodBankAPI.Controllers{

[ApiController]
[Route("api/[controller]")]
public class BloodDonationController : ControllerBase
{
    private readonly IBloodDonationService _donationService;

    public BloodDonationController(IBloodDonationService donationService)
    {
        _donationService = donationService;
    }

    [HttpPost]
    public async Task<IActionResult> RecordDonation([FromBody] BloodDonation donation)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _donationService.RecordDonationAsync(donation);
        return CreatedAtAction(nameof(RecordDonation), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDonations()
    {
        var donations = await _donationService.GetAllDonationsAsync();
        return Ok(donations);
    }
}
}