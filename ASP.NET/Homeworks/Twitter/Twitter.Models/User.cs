using System;

namespace Twitter.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        public User()
        {
            this.Tweets = new HashSet<Tweet>();
            this.FavoritTweets = new HashSet<Tweet>();
            this.ReTweets = new HashSet<Tweet>();
            this.Followers = new HashSet<User>();
            this.Following = new HashSet<User>();
            this.Notifications = new HashSet<Notification>();
            this.JoinOn = DateTime.Now;
        }

        [MinLength(3)]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(25)]
        public string LastName { get; set; }

        public DateTime JoinOn { get; set; }

        public virtual ICollection<Tweet> Tweets { get; set; }

        public virtual ICollection<Tweet> FavoritTweets { get; set; }

        public virtual ICollection<Tweet> ReTweets { get; set; }

        public virtual ICollection<User> Followers { get; set; }

        public virtual ICollection<User> Following { get; set; }

        public virtual ICollection<Replay> Replays { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
