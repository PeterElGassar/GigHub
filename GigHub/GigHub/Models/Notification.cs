using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Notification
    {

        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }

        public DateTime? OldDateTime { get; set; }

        public string OldLocation { get; set; }


        [Required]
        public Gig Gig { get; set; }

    }
}