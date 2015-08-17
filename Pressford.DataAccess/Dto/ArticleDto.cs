using Pressford.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pressford.DataAccess.Dto
{
    public class ArticleDto
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PublishDate { get; set; }
        public User Author { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
        public int LikeCounter { get; set; }
        public bool IsUserPublisher { get; set; }

        public bool IsAlreadyLiked { get; set; }
    }
}
