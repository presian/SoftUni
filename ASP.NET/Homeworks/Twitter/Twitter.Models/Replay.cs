using System;

namespace Twitter.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Replay
    {
        public Replay()
        {
            this.CreatedOn = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [Required]
        public int TweetId { get; set; }

        public virtual Tweet Tweet { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(160)]
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
