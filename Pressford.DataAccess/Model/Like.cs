using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pressford.DataAccess.Model
{
    public class Like
    {
        public int ArticleId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public Article Article { get; set; }
    }
}
