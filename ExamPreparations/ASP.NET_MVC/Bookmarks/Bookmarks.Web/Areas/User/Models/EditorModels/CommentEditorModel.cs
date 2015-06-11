namespace Bookmarks.Web.Areas.User.Models.EditorModels
{
    using System.ComponentModel.DataAnnotations;

    public class CommentEditorModel
    {
        [Required]
        public string Text { get; set; }
    }
}