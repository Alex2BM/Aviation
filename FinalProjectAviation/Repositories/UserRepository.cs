﻿using FinalProjectAviation.Data;
using FinalProjectAviation.Security;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectAviation.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(AviationDBDbContext context) : base(context){ }

        
        public async Task<List<User>> GetAllUsersFilteredAsync(int pageNumber, int pageSize, List<Func<User, bool>> predicate)
        {
           int skip = pageNumber * pageSize;
            IQueryable<User> query = _context.Users.Skip(skip).Take(pageSize);

            if(predicate != null && predicate.Any())
            {
                query = query.Where(u => predicate.All(predicate => predicate(u)));
            }
            return await query.ToListAsync();   
        }



        public async Task<User?> GetByUsernameAsync(string username)
        {
            var user = await _context.Users.Where(u =>u.Username == username).FirstOrDefaultAsync(); 
            return user;
        }


        public async Task<User?> GetUserAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username
                    || x.Email == username);
            if (user == null)
            {
                return null;
            }
            if (!EncryptionUtil.IsValidPassword(password, user.Password!))
            {
                return null;
            }
            return user;
        }

        public Task<User?> UpdateUserAsync(int userId, User request)
        {
            var existingUser = _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if(existingUser == null) return null;
            if (existingUser.Id != userId) return null;

            _context.Users.Attach(request);
            _context.Entry(request).State = EntityState.Modified;

            return existingUser;

        }
    }
}
