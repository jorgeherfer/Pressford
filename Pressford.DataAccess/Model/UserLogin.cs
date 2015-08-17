using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pressford.DataAccess.Model
{
    public class UserLogin
    {
        public virtual int UserId { get; set; }
        public virtual string Password { get; set; }
        public virtual User User { get; set; }

    }
}
