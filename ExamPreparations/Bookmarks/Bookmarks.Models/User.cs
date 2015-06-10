namespace Bookmarks.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        public User()
        {
            this.Votes = new List<Vote>();
            this.Comments = new List<Comment>();
            this.Bookmarks = new List<Bookmark>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Bookmark> Bookmarks { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
