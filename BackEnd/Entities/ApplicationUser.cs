using Microsoft.AspNetCore.Identity;

namespace BackEnd.Entities;

public class ApplicationUser:IdentityUser
{
    public int BuildingNumber { get; set; }
    public int ApartmentNumber { get; set; }
    public bool IsAdmin { get; set; } = false;
    public bool IsApproved { get; set; } = false;
    public bool IsBanned { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
