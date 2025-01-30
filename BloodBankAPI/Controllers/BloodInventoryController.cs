using Microsoft.AspNetCore.Mvc;
using BloodBankAPI.Services;
using BloodBankAPI.Models;
 

namespace BloodBankAPI.Controllers{

[ApiController]
[Route("api/[controller]")]
public class BloodInventoryController : ControllerBase
{
    private readonly IBloodInventoryService _bloodInventoryService;

    public BloodInventoryController(IBloodInventoryService bloodInventoryService)
    {
        _bloodInventoryService = bloodInventoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInventory()
    {
         try
            {
                var inventory = await _bloodInventoryService.GetAllInventoryAsync();
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        /*var inventory = await _bloodInventoryService.GetAllInventoryAsync();
        return Ok(inventory);*/
    }

[HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetInventoryById(string id)
    {
       try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest("Invalid ID provided.");
                }

                var inventory = await _bloodInventoryService.GetInventoryByIdAsync(id);
                if (inventory == null)
                {
                    return NotFound("Inventory not found.");
                }

                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
       /* var inventory = await _bloodInventoryService.GetInventoryByIdAsync(id);
        if (inventory == null)
        {
            return NotFound();
        }
        return Ok(inventory);*/
    }

    [HttpPost]
    public async Task<IActionResult> CreateInventory(Inventory inventory)
    {
        try
            {
                if (inventory == null || string.IsNullOrWhiteSpace(inventory.BloodType) || inventory.Quantity < 1)
                {
                    return BadRequest("Invalid inventory data.");
                }
                if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

                inventory.SetId(inventory.Id);
                await _bloodInventoryService.CreateInventoryAsync(inventory);

                return CreatedAtAction(nameof(GetInventoryById), new { id = inventory.Id }, inventory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        /*inventory.SetId(inventory.Id);
        await _bloodInventoryService.CreateInventoryAsync(inventory);
        return CreatedAtAction(nameof(GetInventoryById), new { id = inventory.Id }, inventory);*/
        
    }

[HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateInventory(string id, [FromBody] Inventory updatedInventory)
    {
         try
            {
                if (string.IsNullOrWhiteSpace(id) || updatedInventory == null)
                {
                    return BadRequest("Invalid input data.");
                }
                  if (!ModelState.IsValid)
                  {
                    return BadRequest(ModelState);
                  }

                var existingInventory = await _bloodInventoryService.GetInventoryByIdAsync(id);
                if (existingInventory == null)
                {
                    return NotFound("Inventory not found.");
                }

                await _bloodInventoryService.UpdateInventoryAsync(id, updatedInventory);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        /*await _bloodInventoryService.UpdateInventoryAsync(id, updatedInventory);
        return NoContent();  // If the update was successful, return 204 No Content*/
    }


      [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteInventory(string id)
    {
        try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest("Invalid ID provided.");
                }

                var existingInventory = await _bloodInventoryService.GetInventoryByIdAsync(id);
                if (existingInventory == null)
                {
                    return NotFound("Inventory not found.");
                }

                await _bloodInventoryService.DeleteInventoryAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        /*var existingInventory = await _bloodInventoryService.GetInventoryByIdAsync(id);
        if (existingInventory == null)
        {
            return NotFound();
        }
        await _bloodInventoryService.DeleteInventoryAsync(id);
        return NoContent();*/
    }
  }
}
