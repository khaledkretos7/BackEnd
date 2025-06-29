using BackEnd.Contracts.User;
using BackEnd.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPendingUser()
    {
        //check admin first
        var isAdmin = User.FindFirst("IsAdmin")?.Value;

        if (isAdmin != "True")
            return Forbid();

        var users = await _userManager.Users
            .Where(u => !u.IsApproved && !u.IsBanned) // pending only
            .Select(u => new UserResponse(
                u.Id,
                u.UserName,
                u.PhoneNumber,
                u.BuildingNumber,
                u.ApartmentNumber,
                u.IsAdmin,
                u.IsApproved,
                u.IsBanned
            )).ToListAsync();

        return Ok(users);
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        var isAdmin = User.FindFirst("IsAdmin")?.Value;

        if (isAdmin != "True")
            return Forbid();

        var users = await _userManager.Users
            .Select(u => new UserResponse(
                u.Id,
                u.UserName,
                u.PhoneNumber,
                u.BuildingNumber,
                u.ApartmentNumber,
                u.IsAdmin,
                u.IsApproved,
                u.IsBanned
            )).ToListAsync();

        return Ok(users);
    }

    [HttpPost("approve/{userId}")]
    public async Task<IActionResult> ApproveUser(string userId)
    {
        var isAdmin = User.FindFirst("IsAdmin")?.Value;

        if (isAdmin != "True")
            return Forbid();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        user.IsApproved = true;
        await _userManager.UpdateAsync(user);

        return Ok(new { message = "User approved successfully." });
    }

    [HttpPost("reject/{userId}")]
    public async Task<IActionResult> RejectUser(string userId)
    {
        var isAdmin = User.FindFirst("IsAdmin")?.Value;

        if (isAdmin != "True")
            return Forbid();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        user.IsBanned = true;
        await _userManager.UpdateAsync(user);

        return Ok(new { message = "User rejected and banned successfully." });
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        //var userResponse = new UserResponse(
        //    user.Id,
        //    user.UserName,
        //    user.PhoneNumber,
        //    user.BuildingNumber,
        //    user.ApartmentNumber,
        //    user.IsAdmin,
        //    user.IsApproved,
        //    user.IsBanned
        //);
        var userResponse = user.Adapt<UserResponse>();

        return Ok(userResponse);
    }
    [HttpPost("set-admin/{userId}")]
    public async Task<IActionResult> SetAdmin(string userId)
    {
        var isAdmin = User.FindFirst("IsAdmin")?.Value;

        if (isAdmin != "True")
            return Forbid();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        user.IsAdmin = true;
        await _userManager.UpdateAsync(user);

        return Ok(new { message = "User is now an admin." });
    }
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var isAdmin = User.FindFirst("IsAdmin")?.Value;

        if (isAdmin != "True")
            return Forbid();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        await _userManager.DeleteAsync(user);

        return Ok(new { message = "User deleted successfully." });
    }

}
