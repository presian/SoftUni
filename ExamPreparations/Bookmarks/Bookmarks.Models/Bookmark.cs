namespace Bookmarks.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Bookmark
    {
        public Bookmark()
        {
            this.Comments = new List<Comment>();
            this.Votes = new List<Vote>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string Url { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        public string Description { get; set; }

        [Required]
        public virtual User User { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
