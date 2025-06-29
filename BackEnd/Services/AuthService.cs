using BackEnd.Contracts.Auth;
using BackEnd.Entities;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Services;

public class AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IJwtTokenGenerator tokenGenerator) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IJwtTokenGenerator _tokenGenerator = tokenGenerator;

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.UserName);
        if (existingUser != null)
            throw new Exception("Username already exists.");

        var user = new ApplicationUser
        {
            UserName = request.UserName,    
            PhoneNumber = request.PhoneNumber,
            BuildingNumber = request.BuildingNumber,
            ApartmentNumber = request.ApartmentNumber,
            IsApproved = false, 
            IsAdmin = false,
            IsBanned = false
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new Exception("Failed to register user.");

        var token = await _tokenGenerator.GenerateToken(user);

        return new AuthResponse(token);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
            throw new Exception("Invalid Username or Password.");

        if (user.IsBanned)
            throw new Exception("Your account has been banned.");

        if (!user.IsApproved)
            throw new Exception("Your account is pending approval.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            throw new Exception("Invalid Username or Password.");

        var token = await _tokenGenerator.GenerateToken(user);

        return new AuthResponse(token);
    }
}
