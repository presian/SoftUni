namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ReportedTweet
    {
        [Key]
        public int Id { get; set; }

        public DateTime ReportedOn { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int TweetId { get; set; }

        public virtual Tweet Tweet { get; set; }
    }
}
