namespace Bookmarks.Web.Areas.User.Models.DisplayModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Bookmarks.Models;

    public class DetailsDisplayModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Ctegory { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public int Votes { get; set; }

        public IEnumerable<CommentDisplayModel> Comments { get; set; }

        public static Expression<Func<Bookmark, DetailsDisplayModel>> ViewModle
        {
            get
            {
                return d => new DetailsDisplayModel
                {
                    Id = d.Id,
                    Description = d.Description,
                    Title = d.Title,
                    Ctegory = d.Category.Name,
                    Url = d.Url,
                    Comments = d.Comments
                        .AsQueryable()
                        .Select(CommentDisplayModel.ViewModel),
                    Votes = d.Votes.Count
                };
            }
        }

        public static DetailsDisplayModel CreateFromBookmark(Bookmark bookmark)
        {
            return  new DetailsDisplayModel
            {
                Id = bookmark.Id,
                Description = bookmark.Description,
                Title = bookmark.Title,
                Ctegory = bookmark.Category.Name,
                Url = bookmark.Url,
                Comments = bookmark.Comments
                    .AsQueryable()
                    .Select(CommentDisplayModel.ViewModel),
                Votes = bookmark.Votes.Count
            };
        }
    }
}