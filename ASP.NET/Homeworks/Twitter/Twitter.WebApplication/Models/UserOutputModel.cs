namespace Twitter.WebApplication.Models
{
    using System;
    using System.Linq.Expressions;
    using Twitter.Models;

    public class UserOutputModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public static Expression<Func<User, UserOutputModel>> ViewModel
        {
            get
            {
                return u => new UserOutputModel
                {
                    FirstName = u.FirstName,
                    Username = u.UserName,
                    Email = u.Email,
                    LastName = u.LastName,
                    Id = u.Id
                };
            }
        }
    }
}