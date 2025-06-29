using BackEnd.Entities;

namespace BackEnd.Services;

public interface IJwtTokenGenerator
{
      Task<string> GenerateToken(ApplicationUser user);
}
