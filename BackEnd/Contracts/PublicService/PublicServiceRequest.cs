namespace BackEnd.Contracts.PublicService;

public record PublicServiceRequest(
    string Name,
    int CategoryId,
    string PhoneNumber,
    string Status);
