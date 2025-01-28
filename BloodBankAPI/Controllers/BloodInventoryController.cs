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
        var inventory = await _bloodInventoryService.GetAllInventoryAsync();
        return Ok(inventory);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInventoryById(string id)
    {
        var inventory = await _bloodInventoryService.GetInventoryByIdAsync(id);
        if (inventory == null)
        {
            return NotFound();
        }
        return Ok(inventory);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInventory(BloodInventory inventory)
    {
        await _bloodInventoryService.CreateInventoryAsync(inventory);
        return CreatedAtAction(nameof(GetInventoryById), new { id = inventory.Id }, inventory);
    }

    [HttpPut("{Type}")]
    public async Task<IActionResult> UpdateInventory(string bloodType, int quantityInUnits)
    {
        var existingInventory = await _bloodInventoryService.GetInventoryByIdAsync(bloodType);
        if (existingInventory == null)
        {
            return NotFound();
        }
        await _bloodInventoryService.UpdateInventoryAsync(bloodType, quantityInUnits);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInventory(string id)
    {
        var existingInventory = await _bloodInventoryService.GetInventoryByIdAsync(id);
        if (existingInventory == null)
        {
            return NotFound();
        }
        await _bloodInventoryService.DeleteInventoryAsync(id);
        return NoContent();
    }
  }
}