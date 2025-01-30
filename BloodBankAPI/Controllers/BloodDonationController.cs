using Microsoft.AspNetCore.Mvc;
using BloodBankAPI.Services;
using BloodBankAPI.Models;
using MongoDB.Bson;

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
        public async Task<ActionResult<List<Donation>>> GetAllDonations(int page, int pageSize)
        {
            try
            {
                if (page < 1 || pageSize < 1)
                {
                    return BadRequest("Page and pageSize must be greater than zero.");
                }

                var donations = await _donationService.GetAllDonationsPaginatedAsync(page, pageSize);
                if (donations == null || donations.Count == 0)
                {
                    return NotFound("No donations found.");
                }
                return Ok(donations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching donations: {ex.Message}");
            }
        }


    [HttpPost]
public async Task<ActionResult<Donation>> CreateDonation([FromBody] Donation donation)
{   
   try
    {
        if (donation == null)
        {
            return BadRequest("Donation data is required.");
        }
        if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (string.IsNullOrWhiteSpace(donation.DonorId) || !ObjectId.TryParse(donation.DonorId, out _))
            {
                return BadRequest(new { message = "Invalid Donor ID format. It must be a 24-character hex string." });
            }
        donation.SetId(donation.Id); 

        var createdDonation = await _donationService.CreateDonationAsync(donation);

        return CreatedAtAction(nameof(GetAllDonations), new { id = createdDonation.Id }, createdDonation);
    }
    catch (ArgumentException ex)
    {
        return BadRequest($"Invalid input: {ex.Message}");
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"An error occurred while creating the donation: {ex.Message}");
    }
    /*if (donation == null)
    {
        return BadRequest("Donation data is required.");
    }
    donation.SetId(donation.Id);
    
    var createdDonation = await _donationService.CreateDonationAsync(donation);
    return CreatedAtAction(nameof(GetAllDonations), new { id = createdDonation.Id }, createdDonation);*/
}
   
}
}

