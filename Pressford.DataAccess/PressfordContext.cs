using Pressford.DataAccess.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;

namespace Pressford.DataAccess
{
    public class PressfordContext : DbContext
    {
        public PressfordContext() : base("PressfordContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(user => user.UserId);
            modelBuilder.Entity<User>().Property(user => user.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>().Property(user => user.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(t => t.Name).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute() {IsUnique = true }));
            modelBuilder.Entity<Comment>().HasKey(comment => comment.CommentId);
            modelBuilder.Entity<Comment>().HasRequired(comment => comment.Article).WithMany(article =>article.Comments).HasForeignKey(comment => comment.ArticleId).WillCascadeOnDelete();
            modelBuilder.Entity<Comment>().Property(comment => comment.CommentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Article>().HasKey(article => article.ArticleId);
            modelBuilder.Entity<Article>().Property(article => article.ArticleId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Like>().HasKey(like => new { like.ArticleId, like.UserId });
            modelBuilder.Entity<UserLogin>().HasRequired(userLogin => userLogin.User).WithRequiredDependent();
            modelBuilder.Entity<UserLogin>().HasKey(userLogin => userLogin.UserId);
            modelBuilder.Entity<UserLogin>().Property(userLogin => userLogin.Password).IsRequired();
        }
    }
}
