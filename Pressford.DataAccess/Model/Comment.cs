using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pressford.DataAccess.Model
{
    public class Comment
    {
        public int CommentId { get; set; }

        public virtual int ArticleId { get; set; }
        public virtual User Author { get; set; }
        public virtual Article Article { get; set; }
        public DateTime PublishDate { get; set; }
        public string Content { get; set; }
    }
}
