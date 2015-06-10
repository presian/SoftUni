namespace Bookmarks.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [Required]
        public string Text { get; set; }


        public int BookmarkId { get; set; }

        [Required]
        public virtual Bookmark Bookmark { get; set; }
    }
}
