namespace Bookmarks.Web.Areas.User.Models.EditorModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Bookmarks.Models;

    public class BookmarkEditorModel
    {
        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        [DisplayName("Category")]
        public ICollection<Category> Categories { get; set; }
    }
}