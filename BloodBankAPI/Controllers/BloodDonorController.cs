using BloodBankAPI.Models;
using BloodBankAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        [HttpPost]
    public async Task <IActionResult> Create([FromBody] Donor donor)
    {
        try
            {
                // Input validation
                if (donor == null || string.IsNullOrEmpty(donor.BloodType))
                {
                    return BadRequest("Donor data and blood type are required.");
                }

                // Set unique ID for the donor
                donor.SetId(donor.Id);

                // Call service to create the donor
                await _donorService.CreateDonorAsync(donor);
                return CreatedAtAction(nameof(GetById), new { id = donor.Id }, donor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating donor: {ex.Message}");
            }
        /*if (donor == null  string.IsNullOrEmpty(donor.BloodType))
        {
            return BadRequest("Donor data is required and blood type is required.");
        }
        donor.SetId(donor.Id);
        await _donorService.CreateDonorAsync(donor);
        return Ok( donor);*/
    }


        [HttpGet("{id}")]
        public async Task<ActionResult<Donor>> GetById(string id)
        {   
            try
            {
                // Check if ID is valid
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Donor ID is required.");
                }

                // Get donor by ID
                var donor = await _donorService.GetDonorByIdAsync(id);
                if (donor == null)
                {
                    return NotFound($"Donor with ID {id} not found.");
                }

                donor.SetId(donor.Id);
                return Ok(donor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving donor: {ex.Message}");
            }
            /*var donor = await _donorService.GetDonorByIdAsync(id);
            donor.SetId(donor.Id);
            return Ok(donor);*/
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donor>>> GetAll()
        {   
             try
            {
                var donors = await _donorService.GetAllDonorsAsync();
                if (donors == null || !donors.Any())
                {
                    return NotFound("No donors found.");
                }

                return Ok(donors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving donors: {ex.Message}");
            }
            /*var donors = await _donorService.GetAllDonorsAsync();
            return Ok(donors);*/
        }

       [HttpPut("{id}")]
       public async Task<ActionResult<Donor>> Update(string id, [FromBody] Donor donor)
        {
            try
            {
                if (donor == null)
                {
                    return BadRequest("Donor data is required.");
                }

                var existingDonor = await _donorService.GetDonorByIdAsync(id);
                if (existingDonor == null)
                {
                    return NotFound($"Donor with ID {id} not found.");
                }

                await _donorService.UpdateDonorAsync(id, donor);
                var updatedDonor = await _donorService.GetDonorByIdAsync(id);
                updatedDonor.SetId(updatedDonor.Id);
                return Ok(updatedDonor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating donor: {ex.Message}");
            }
        }

    }

}


    
       