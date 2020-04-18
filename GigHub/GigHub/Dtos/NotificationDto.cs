using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Dtos
{
    public class NotificationDto
    {

        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }

        public DateTime? OldDateTime { get; set; }

        public string OldLocation { get; set; }

        public GigDto Gig { get; set; }
        public bool IsRead { get; set; }
    }
}