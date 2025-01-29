using API.DTOs;
using API.DTOs.Auth;
using API.Models;

namespace API.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task RegisterUser(RegisterUserDto registerUserDto);
    Task Login(LoginUserDto loginUserDto);
}