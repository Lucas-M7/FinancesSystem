using API.Data;
using API.DTOs;
using API.DTOs.Auth;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations;

public class UserRepository(
    FinanceAppContext context,
    SignInManager<User> signInManager,
    UserManager<User> userManager)
    : Repository<User>(context), IUserRepository
{
    public async Task RegisterUser(RegisterUserDto registerUserDto)
    {
        var user = CreateUser(registerUserDto);
        
        var result = await userManager.CreateAsync(user, registerUserDto.Password);
        
        if (!result.Succeeded)
            HandleRegistrationFailure(result);
    }

    public async Task Login(LoginUserDto loginUserDto)
    {
        var result = await SignInUser(loginUserDto);
        
        if (!result.Succeeded)
            HandleLoginFailure();
    }

    #region PrivateFunctions
    private async Task<SignInResult> SignInUser(LoginUserDto loginUserDto)
    {
        return await signInManager.PasswordSignInAsync(
            loginUserDto.Email, loginUserDto.Password, loginUserDto.RememberMe, false);
    }

    private static User CreateUser(RegisterUserDto registerUserDto)
    {
        return new User
        {
            Email = registerUserDto.Email,
            UserName = registerUserDto.FullName
        };
    }

    private static void HandleLoginFailure()
    {
        throw new UnauthorizedAccessException("Invalid login attempt.");
    }

    private static void HandleRegistrationFailure(IdentityResult result)
    {
        var erros = string.Join(", ", result.Errors.Select(e => e.Description));
        throw new ArgumentException($"Failed to register user: {erros}");
    }
    #endregion
}