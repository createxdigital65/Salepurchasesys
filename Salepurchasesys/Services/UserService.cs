using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalePurchasesys.Data;
using SalePurchasesys.DTOs;
using SalePurchasesys.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalePurchasesys.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUserAsync(int id, User user)
        {
            var existing = await _context.Users.FindAsync(id);
            if (existing == null) return null;

            _mapper.Map(user, existing);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(existing);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }
    }
}
