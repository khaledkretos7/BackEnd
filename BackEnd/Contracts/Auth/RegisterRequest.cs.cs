namespace BackEnd.Contracts.Auth;
public record RegisterRequest(
    string UserName,
    string Password,
    string PhoneNumber,
    int BuildingNumber,
    int ApartmentNumber
);
