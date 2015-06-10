namespace Bookmarks.Web.Models
{
    using System;
    using System.Linq.Expressions;

    using Bookmarks.Models;

    using Microsoft.Ajax.Utilities;

    public class BookmarkDisplayModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public static Expression<Func<Bookmark, BookmarkDisplayModel>> ViewModel
        {
            get
            {
                return b => new BookmarkDisplayModel
                {
                    Id = b.Id,
                    Description = b.Description,
                    Title = b.Title
                };
            }
        }
    }
}