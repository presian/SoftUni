namespace Bookmarks.Web.Areas.User.Models.DisplayModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Bookmarks.Models;

    public class DetailsDisplayModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Ctegory { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

//        public ICollection<Vote> Votes { get; set; }
//
//        public ICollection<Comment> Comments { get; set; }

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
                    Url = d.Url
                };
            }
        }
    }
}