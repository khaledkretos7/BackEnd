namespace BackEnd.Contracts.PublicService;

public record PublicServiceResponse
(
    int Id,
    string Name,
    string PhoneNumber,
    string Status,
    int CategoryId
);
