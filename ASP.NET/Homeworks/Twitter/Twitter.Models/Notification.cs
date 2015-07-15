namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public NotificationType Type { get; set; }

        public bool IsNew { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}
