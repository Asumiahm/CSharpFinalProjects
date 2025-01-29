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
        
        if (donor == null || string.IsNullOrEmpty(donor.BloodType))
        {
            return BadRequest("Donor data is required and blood type is required.");
        }
        donor.SetId(donor.Id);
        await _donorService.CreateDonorAsync(donor);
        return Ok( donor);
    }


        [HttpGet("{id}")]
        public async Task<ActionResult<Donor>> GetById(string id)
        {
            var donor = await _donorService.GetDonorByIdAsync(id);
            donor.SetId(donor.Id);
            return Ok(donor);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donor>>> GetAll()
        {
            var donors = await _donorService.GetAllDonorsAsync();
            return Ok(donors);
        }

       [HttpPut("{id}")]
       public async Task<ActionResult<Donor>> Update(string id, Donor donor)
       {
           
           await _donorService.UpdateDonorAsync(id, donor);
           var updatedDonor = await _donorService.GetDonorByIdAsync(id);
           updatedDonor.SetId(updatedDonor.Id); 
           return Ok(updatedDonor);
       }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonor(string id)
        {
            var donor = await _donorService.GetDonorByIdAsync(id);
            donor.SetId(donor.Id);
            if (donor == null)
            {
                return NotFound();
            }
            await _donorService.DeleteDonorAsync(id);
            return NoContent();
        }
    }
}
