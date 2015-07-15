namespace Twitter.WebApplication.Areas.Admin.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using Antlr.Runtime.Misc;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class RoleEditModel
    {
        [Key]
        public string Id { get; set; }
        
        [Required]

        public string Name { get; set; }

        public static Expression<Func<IdentityRole, RoleEditModel>> ViewModel
        {
            get
            {
                return r => new RoleEditModel
                {
                    Id = r.Id,
                    Name = r.Name
                };
            }
        }
    }
}