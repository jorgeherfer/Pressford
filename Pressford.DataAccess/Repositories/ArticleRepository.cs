using Pressford.DataAccess.Dto;
using Pressford.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pressford.DataAccess.Repositories
{
    public class ArticleRepository
    {
        public async Task<IEnumerable<ArticleDto>> GetArticlesAsync()
        {
            using (var context = new PressfordContext())
            {
                var articlesQuery = from article in context.Articles
                                    select new ArticleDto
                                    {
                                        ArticleId = article.ArticleId,
                                        Title = article.Title,
                                        Body = article.Body,
                                        PublishDate = article.PublishDate,
                                        Author = article.Author,
                                        Comments = article.Comments.Select(comment => new CommentDto { AuthorName = comment.Author.Name, Content = comment.Content }).ToList(),
                                        LikeCounter = article.Likes.Count,
                                        IsUserPublisher = System.Threading.Thread.CurrentPrincipal.Identity.Name == article.Author.Name,
                                        IsAlreadyLiked = article.Likes.Any(like => like.User.Name == System.Threading.Thread.CurrentPrincipal.Identity.Name)
                                    };
                return await articlesQuery.ToListAsync();
            }
        }

        public async Task LikeArticleAsync(int articleId, int userId)
        {
            using (var context = new PressfordContext())
            {
                context.Likes.Add(new Like
                {
                    ArticleId = articleId,
                    UserId = userId
                });
                await context.SaveChangesAsync();
            }
        }

        public async Task AddCommentToArticleAsync(int articleId, int userId, string comment)
        {
            using (var context = new PressfordContext())
            {
                var article = context.Articles.Attach(new Article { ArticleId = articleId });
                var user = context.Users.Attach(new User { UserId = userId });
                context.Comments.Add(new Comment
                {
                    Article = article,
                    Author = user,
                    PublishDate = DateTime.Now,
                    Content = comment
                });
                await context.SaveChangesAsync();
            }
        }

        public async Task<ArticleDto> GetArticleAsync(int articleId)
        {
            if (articleId == 0)
            {
                throw new ArgumentException("articleId");
            }

            using (var context = new PressfordContext())
            {
                var articlesQuery = from article in context.Articles
                                    where article.ArticleId == articleId
                                    select new ArticleDto
                                    {
                                        ArticleId = article.ArticleId,
                                        Title = article.Title,
                                        Body = article.Body,
                                        PublishDate = article.PublishDate,
                                        Author = article.Author,
                                        Comments = article.Comments.Select(comment => new CommentDto { AuthorName = comment.Author.Name, Content = comment.Content }).ToList(),
                                        LikeCounter = article.Likes.Count,
                                        IsUserPublisher = System.Threading.Thread.CurrentPrincipal.Identity.Name == article.Author.Name,
                                        IsAlreadyLiked = article.Likes.Any(like => like.User.Name == System.Threading.Thread.CurrentPrincipal.Identity.Name)
                                    };
                return await articlesQuery.SingleOrDefaultAsync();
            }
        }

        public async Task UpdateArticleAsync(ArticleDto article)
        {
            using (var context = new PressfordContext())
            {
                var articleToUpdate = await context.Articles.SingleAsync(art => art.ArticleId == article.ArticleId);
                articleToUpdate.Title = article.Title;
                articleToUpdate.Body = article.Body;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteArticleAsync(int articleId)
        {
            using (var context = new PressfordContext())
            {
                var article = context.Articles.Attach(new Article { ArticleId = articleId });
                context.Articles.Remove(article);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddArticleAsync(ArticleDto article)
        {
            using (var context = new PressfordContext())
            {
                context.Users.Attach(article.Author);
                context.Articles.Add(new Article
                {
                    Author = article.Author,
                    Title = article.Title,
                    Body = article.Body,
                    PublishDate = DateTime.Now
                }
                    );
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ArticleLikesDto>> GetArticleWithMostLikesAsync(int limit = 100)
        {
            using (var context = new PressfordContext())
            {
                var articlesQuery = from article in context.Articles
                                    orderby article.Likes.Count descending
                                    select new ArticleLikesDto
                                    {
                                        ArticleTitle = article.Title,
                                        LikeCounter = article.Likes.Count
                                    };
                return await articlesQuery.Take(limit).ToListAsync();
            }
        }

    }
}