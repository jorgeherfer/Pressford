using Pressford.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pressford.DataAccess
{
    public class PressfordInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PressfordContext>
    {
        protected override void Seed(PressfordContext context)
        {
            var articles = new List<Article>
            {
            new Article
            {
                Title ="Article 1",
                Body ="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                PublishDate =DateTime.Now,
                Author = new User() { UserId = 1, Name = "DemoPublisher", UserType = UserType.Publisher }
            }
            };

            var userLogins = new List<UserLogin>
            {
            new UserLogin
            {
                UserId = 1,
                Password = "1234"
            },
            new UserLogin
            {
                User =  new User {UserId = 2, Name = "DemoEmployee", UserType = UserType.Employee },
                Password = "1234"
            },
            new UserLogin
            {
                User =  new User {UserId = 3, Name = "DemoEmployee2", UserType = UserType.Employee },
                Password = "1234"
            },
            new UserLogin
            {
                User =  new User {UserId = 4, Name = "DemoEmployee3", UserType = UserType.Employee },
                Password = "1234"
            }
            };

            context.Articles.AddRange(articles);
            context.UserLogins.AddRange(userLogins);
            context.SaveChanges();
        }
    }
}
