using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppApi.DbManagement.Interfaces;
using AppApi.Model.User;
using webApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.DbManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region D-I
        private contactContext db;
        private IMemoryCache cache;
        public UserRepository(contactContext _db, IMemoryCache _cache)
        {
            cache = _cache;
            db = _db;
        }
        #endregion

        #region Repository
        public async Task<User> AddUser(User user)
        {
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<bool> IsExists(User user)
        {
            return await db.Users.AnyAsync(p => p.FullName == user.FullName || p.Phone == user.Phone || p.Id == user.Id);
        }

        public async Task<User> RemoveUser(int id)
        {
            var user = await db.Users.SingleAsync(p => p.Id == id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> Search(string data)
        {
            var FindCache = cache.Get<List<User>>(data);
            if (FindCache is null)
            {
                var Results = await db.Users.Where(p => p.FullName.Contains(data) || p.Phone.Contains(data) || p.Email.Contains(data)).ToListAsync();
                var SetCache = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(100));
                foreach (var res in Results)
                {
                    cache.Set(res.Id, res, SetCache);
                }
                return Results;
            }
            else
            {
                return FindCache;
            }
        }

        public async Task<User> SearchByID(int id)
        {
            var FindCache = cache.Get<User>(id);
            if (FindCache is null)
            {
                var Results = await db.Users.SingleOrDefaultAsync(user => user.Id == id);
                if (Results is not null)
                {
                    var SetCache = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(100));
                    cache.Set(Results.Id, Results, SetCache);
                    return Results;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return FindCache;
            }
        }

        public async Task<User> UpdateUser(User user, int id)
        {
            var finduser = await db.Users.SingleOrDefaultAsync(p => p.Id == id);
            if (finduser is not null)
            {
                if (user.Email is not null)
                {
                    finduser.Email = user.Email;
                }

                if (user.FullName is not null)
                {
                    finduser.FullName = user.FullName;
                }

                if (user.Phone is not null)
                {
                    finduser.Phone = user.Phone;
                }

                if (user.Note is not null)
                {
                    finduser.Note = user.Note;
                }
                await db.SaveChangesAsync();
            }

            return user;
        }

        #endregion
    }
}
