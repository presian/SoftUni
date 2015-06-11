namespace Bookmarks.Web.Areas.User.Models.DisplayModels
{
    using System;
    using System.Linq.Expressions;
    using Bookmarks.Models;

    public class CommentDisplayModel
    {
        public int Id { get; set; }
        
        public string Content { get; set; }

        public string Author { get; set; }

        public static Expression<Func<Comment, CommentDisplayModel>> ViewModel
        {
            get
            {
                return c => new CommentDisplayModel
                {
                    Id = c.Id,
                    Author = c.Author.UserName,
                    Content = c.Text
                };
            }
        }

        public static CommentDisplayModel CreateFromComment(Comment comment)
        {
            return new CommentDisplayModel
            {
                Id = comment.Id,
                Author = comment.Author.UserName,
                Content = comment.Text
            };
        }
    }
}