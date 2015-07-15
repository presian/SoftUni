namespace Twitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tweet
    {
        public Tweet()
        {
            this.Replays = new HashSet<Replay>();
            this.UsersFavorite = new HashSet<User>();
            this.UsersReTweets = new HashSet<User>();
            this.CreatedOn = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(160)]
        public string Text { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public DateTime CreatedOn { get; set; } 

        public virtual ICollection<Replay> Replays { get; set; }

        public virtual ICollection<User> UsersFavorite { get; set; }

        public virtual ICollection<User> UsersReTweets { get; set; }
    }
}
