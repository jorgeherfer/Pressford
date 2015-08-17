using Pressford.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pressford.DataAccess.Repositories
{
    public class UserRepository
    {
        public User GetUser(int userId)
        {
            if (userId == 0)
            {
                throw new ArgumentException("userId");
            }

            using (var context = new PressfordContext())
            {
                var usersQuery = from user in context.Users
                                    where user.UserId == userId
                                    select user;
                return  usersQuery.SingleOrDefault();
            }
        }

        public User GetUser(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("userName");
            }

            using (var context = new PressfordContext())
            {
                var usersQuery = from user in context.Users
                                 where user.Name == userName
                                 select user;
                return usersQuery.SingleOrDefault();
            }
        }

        public async Task<User> GetUserAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("userName");
            }

            using (var context = new PressfordContext())
            {
                var usersQuery = from user in context.Users
                                 where user.Name == userName
                                 select user;
                return await usersQuery.SingleOrDefaultAsync();
            }
        }

        public async Task<int> GetNumberOfLikesAsync(int userId)
        {
            using (var context = new PressfordContext())
            {
                var likesQuery = from like in context.Likes
                             where like.UserId == userId
                             select like;
                return await likesQuery.CountAsync();
            }
        }

        public User GetUser(string userName, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("userName");
            }

            if (string.IsNullOrWhiteSpace(hashedPassword))
            {
                throw new ArgumentException("hashedPassword");
            }

            using (var context = new PressfordContext())
            {
                var usersQuery = from userLogin in context.UserLogins
                                 where userLogin.User.Name == userName
                                 && userLogin.Password == hashedPassword
                                 select userLogin.User;
                return usersQuery.SingleOrDefault();
            }
        }

        public void AddUser(UserLogin userLogin)
        {
            if (userLogin == null)
            {
                throw new ArgumentException("userLogin");
            }

            using (var context = new PressfordContext())
            {
                context.UserLogins.Add(userLogin);
                context.SaveChanges();
            }
        }
    }
}
