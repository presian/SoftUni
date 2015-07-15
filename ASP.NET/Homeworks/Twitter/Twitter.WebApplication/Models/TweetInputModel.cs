namespace Twitter.WebApplication.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TweetInputModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(160)]
        public string Text { get; set; }
    }
}