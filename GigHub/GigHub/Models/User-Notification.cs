using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class User_Notification
    {

        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }


        [Required]
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }

        public ApplicationUser User { get; set; }

        public Notification Notification { get; set; }


        public bool IsRead { get; set; }

    }
}