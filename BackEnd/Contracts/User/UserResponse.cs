namespace BackEnd.Contracts.User;

public record UserResponse(
    string Id,
    string UserName,
    string PhoneNumber,
    int BuildingNumber,
    int ApartmentNumber,
    bool IsAdmin,
    bool IsApproved,
    bool IsBanned
);
