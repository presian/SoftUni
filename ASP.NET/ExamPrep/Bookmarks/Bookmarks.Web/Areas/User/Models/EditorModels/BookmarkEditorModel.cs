namespace Bookmarks.Web.Areas.User.Models.EditorModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Bookmarks.Models;

    public class BookmarkEditorModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        [DisplayName("Title *")]
        public string Title { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        [DisplayName("Url *")]
        public string Url { get; set; }

        public string Description { get; set; }

        [DisplayName("Category *")]
        public int CategoryId { get; set; }
    }
}