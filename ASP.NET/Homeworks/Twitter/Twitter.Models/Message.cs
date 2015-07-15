using System;

namespace Twitter.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        public Message()
        {
            this.CreatedOn = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(500)]
        public string Text { get; set; }

        [Required]
        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public virtual User Recipient { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
