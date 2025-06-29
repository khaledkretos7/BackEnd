using BackEnd.Contracts.PublicService;
using BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublicServiceController(IPublicServiceService service) : ControllerBase
{
    private readonly IPublicServiceService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAllServicesByCategory()
    {
        var result = await _service.GetAllServicesByCategoryAsync();
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Create(PublicServiceRequest request)
    {
        try
        {
            var result = await _service.CreateAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Update(int id, PublicServiceRequest request)
    {
        try
        {
            var result = await _service.UpdateAsync(id, request);
            if (result is null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success)
            return NotFound();

        return Ok(new { message = "Service deleted successfully." });
    }
}
