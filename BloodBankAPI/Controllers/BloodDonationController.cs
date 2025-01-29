using Microsoft.AspNetCore.Mvc;
using BloodBankAPI.Services;
using BloodBankAPI.Models;


namespace BloodBankAPI.Controllers{

[ApiController]
[Route("api/[controller]")]
public class DonationController : ControllerBase
{
    private readonly IDonationService _donationService;

    public DonationController(IDonationService donationService)
    {
        _donationService = donationService;
    }

    [HttpGet]
public async Task<ActionResult<List<Donation>>> GetAllDonations()
{
    var donations = await _donationService.GetAllDonationsAsync();
    if (donations == null || donations.Count == 0)
    {
        return NotFound("No donations found.");
    }
    return Ok(donations);
}


    [HttpPost]
public async Task<ActionResult<Donation>> CreateDonation([FromBody] Donation donation)
{
    if (donation == null)
    {
        return BadRequest("Donation data is required.");
    }

    // Create the donation
    var createdDonation = await _donationService.CreateDonationAsync(donation);
    return CreatedAtAction(nameof(GetAllDonations), new { id = createdDonation.Id }, createdDonation);
}
}
}