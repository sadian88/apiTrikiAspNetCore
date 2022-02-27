using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Triki.CI.Models;

namespace Triki.Data.Mysql.Operations
{
    public class UserDB
    {
        private readonly DbContextSqlTriki db;

        public UserDB(DbContextSqlTriki context)
        {
            db = context;
        }

        public async Task<List<User>> All()
        {
            return await db.User.ToListAsync();
        }


        public  User Search(string email)
        {
            return  db.User.Where(o=>o.Email.Contains(email)).First();
        }

        public async Task<Boolean> Auth(string email, string password)
        {
            var result = await db.User.Where(u=>u.Email == email && u.Password == password).ToListAsync();
            return (result.Count==1);
        }

        public async Task<ActionResult<User>> AddOne(User user)
        {
            db.User.Add(user);
            if (await db.SaveChangesAsync() > 0) return user;
            return null;
        }

        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = await db.User.FirstOrDefaultAsync(x => x.Id == id);

            db.User.Remove(user);
            db.SaveChanges();
            return user;
        }

        public async Task<ActionResult<User>> UpdateOne(int id, User user)
        {
            User resp = new User();
            db.Entry(user).State = EntityState.Modified;
            try
            {
                if (await db.SaveChangesAsync() > 0) return user;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.Write(e.Message);
                if (id != user.Id) return null;
            }

            return resp;
        }
    }
}
