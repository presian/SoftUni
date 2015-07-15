namespace Twitter.WebApplication.Models
{
    using System;
    using System.Linq.Expressions;
    using Twitter.Models;

    public class NotificationOutputModel
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public NotificationType Type { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public static Expression<Func<Notification, NotificationOutputModel>> ViewModel
        {
            get
            {
                return n => new NotificationOutputModel
                {
                    Id = n.Id,
                    AuthorName = n.Author.UserName,
                    Content = n.Content,
                    Date = n.Date,
                    Type = n.Type
                };
            }
        }
    }
}