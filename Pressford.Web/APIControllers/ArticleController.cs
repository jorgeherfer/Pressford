using Pressford.DataAccess.Dto;
using Pressford.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace Pressford.Web.APIControllers
{
    [Authorize]
    public class ArticleController : ApiController
    {
        private const int MaxNumberOfLikesPerUser = 3;
        private readonly ArticleRepository articleRepository;
        private readonly UserRepository userRepository;
        public ArticleController()
        {
            articleRepository = new ArticleRepository();
            userRepository = new UserRepository();
        }

        // GET: api/article
        public async Task<IEnumerable<ArticleDto>> Get()
        {
            return await articleRepository.GetArticlesAsync();
        }


        [Route("api/article/mostLiked")]
        public async Task<IEnumerable<ArticleLikesDto>> GetMostLiked()
        {
            return await articleRepository.GetArticleWithMostLikesAsync();
        }

        [Route("api/article/like/{articleId}")]
        [HttpPut]
        public async Task<IHttpActionResult> LikeArticle(int articleId)
        {
            var user = await userRepository.GetUserAsync(User.Identity.Name);
            if (await userRepository.GetNumberOfLikesAsync(user.UserId) <= MaxNumberOfLikesPerUser)
            {
                await articleRepository.LikeArticleAsync(articleId, user.UserId);
                return Ok();
            }
            return StatusCode(HttpStatusCode.Forbidden);
        }

        [Route("api/article/addComment/{articleId}")]
        [HttpPost]
        public async Task<IHttpActionResult> AddCommentToArticle(int articleId, [FromBody]string comment)
        {
            var user = await userRepository.GetUserAsync(User.Identity.Name);
            await articleRepository.AddCommentToArticleAsync(articleId, user.UserId, comment);
            return Ok();
        }

        // POST: api/article
        public async Task<IHttpActionResult> Post([FromBody]ArticleDto article)
        {
            var user = await userRepository.GetUserAsync(User.Identity.Name);
            if (user.UserType!=DataAccess.Model.UserType.Publisher)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            article.Author = user;
            await articleRepository.AddArticleAsync(article);
            return Ok();
        }

        // PUT: api/article/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]ArticleDto article)
        {
            var user = await userRepository.GetUserAsync(User.Identity.Name);
            if (user.UserType != DataAccess.Model.UserType.Publisher)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            await articleRepository.UpdateArticleAsync(article);
            return Ok();

        }

        // DELETE: api/article/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var user = await userRepository.GetUserAsync(User.Identity.Name);
            if (user.UserType != DataAccess.Model.UserType.Publisher)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            await articleRepository.DeleteArticleAsync(id);
            return Ok();
        }
    }
}
