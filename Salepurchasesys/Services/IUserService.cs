using SalePurchasesys.DTOs;
using SalePurchasesys.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(User user);
        Task<UserDto> UpdateUserAsync(int id, User user);
        Task<bool> DeleteUserAsync(int id);
        Task<UserDto> GetUserByEmailAsync(string email);
    }
}
