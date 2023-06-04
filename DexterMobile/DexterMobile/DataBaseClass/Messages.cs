namespace DesktopProject_V3.DataBaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Messages
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Messages(string sender, string recipient, string message )
        {
            this.Sender = sender;
            this.Recipient = recipient;
            this.Message = message;
        }
        public Messages(string sender, string recipient)
        {
            this.Sender = sender;
            this.Recipient = recipient;
        }
        public Messages() { }
        [Key]
        public int MessageId { get; set; }

        [Required]
        [StringLength(45)]
        public string Sender { get; set; }

        [Required]
        [StringLength(45)]
        public string Recipient { get; set; }

        [StringLength(800)]
        public string Message { get; set; }

    }
}

