namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Follow
    {
        public Follow()
        {
            this.FollowedOn = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public virtual User User { get; set; }

        public virtual User Follower { get; set; }

        public DateTime FollowedOn { get; set; }
    }
}
